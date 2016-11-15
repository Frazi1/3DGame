using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _3DGame
{
    public interface IGameObject : ICollidable
    {
        Model Model { get; set; }
        Matrix RotationMatrix { get; set; }
        Vector3 Position { get; set; }

        Matrix[] Transforms { get; set;} 

    }
}