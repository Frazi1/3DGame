using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _3DGame
{
    public class FirstPersonCamera: Camera
    {
        #region Локальные свойства
        private float leftRightRot = 0;       // Угол поворота по оси Y
        private float _upDownRot = 0;
        private float upDownRot           // Угол поворота по оси X
        {
            get { return _upDownRot; }
            set
            {
                if ((value < 1) && (value > -1))
                {
                    _upDownRot = value;
                }
            }
        }
        private MouseState originalMouseState;
        #endregion

        #region Глобальные свойства
        public float rotationSpeed = 0.05f;     // Скорость угла поворота
        public float translationSpeed = 100f;    // Скорость перемещения
        #endregion

        #region Конструктор
        public FirstPersonCamera(Game game) : base(game)
        {
        }
        #endregion

        #region Изменение объекта

        #region Цикл обновления
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float amount = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

            #region Изменение направления цели камера при помощи мыши
            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;

                leftRightRot -= rotationSpeed * xDifference * amount;
                //System.Diagnostics.Debug.WriteLine("leftRightRot=" + leftRightRot);

                upDownRot -= rotationSpeed * yDifference * amount;
                //System.Diagnostics.Debug.WriteLine("upDownRot=" + upDownRot);

                Mouse.SetPosition(Game.GraphicsDevice.Viewport.Width / 2, Game.GraphicsDevice.Viewport.Height / 2);
                UpdateViewMatrix();
                originalMouseState = Mouse.GetState();
            }
            #endregion

            #region Изменение пространственного положения камеры при помощи клавиатуры
            Vector3 moveVector = new Vector3(0, 0, 0);
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.W))
                moveVector += new Vector3(0, 0, -1);
            if (keyState.IsKeyDown(Keys.S))
                moveVector += new Vector3(0, 0, 1);
            if (keyState.IsKeyDown(Keys.D))
                moveVector += new Vector3(1, 0, 0);
            if (keyState.IsKeyDown(Keys.A))
                moveVector += new Vector3(-1, 0, 0);
            if (keyState.IsKeyDown(Keys.Q))
                moveVector += new Vector3(0, 1, 0);
            if (keyState.IsKeyDown(Keys.Z))
                moveVector += new Vector3(0, -1, 0);

            if (keyState.IsKeyDown(Keys.Left))
                leftRightRot += rotationSpeed;
            if (keyState.IsKeyDown(Keys.Right))
                leftRightRot -= rotationSpeed;
            if (keyState.IsKeyDown(Keys.Up))
                upDownRot += rotationSpeed;
            if (keyState.IsKeyDown(Keys.Down))
                upDownRot -= rotationSpeed;
            #endregion

            AddToCameraPosition(moveVector * amount);
        }
        #endregion

        #region Изменение положения камеры и направления смотра
        private void AddToCameraPosition(Vector3 vectorToAdd)
        {
            Matrix cameraRotation = Matrix.CreateFromYawPitchRoll(leftRightRot, upDownRot, 0);
            Vector3 rotatedVector = Vector3.Transform(vectorToAdd, cameraRotation);
            CamPosition+= translationSpeed * (rotatedVector);
            CamTarget += translationSpeed * (rotatedVector);
            UpdateViewMatrix();
        }
        #endregion

        #region Обновление матрицы вида
        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateFromYawPitchRoll(leftRightRot, upDownRot, 0);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);

            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = CamPosition + cameraRotatedTarget;

            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            ViewMatrix = Matrix.CreateLookAt(CamPosition, cameraFinalTarget, cameraRotatedUpVector);
        }
        #endregion
        #endregion

    }
}
