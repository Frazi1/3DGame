using System;
using Microsoft.Xna.Framework;

namespace _3DGame
{
    public class Level
    {
        private int currentLevel;
        private double currentLevelTime;
        private TimeSpan elapsedLevelTime;

        private TimeSpan elapsedBlockSpawned;

        public Level()
        {
            CurrentLevel = 1;
            CurrentLevelTime = Settings.FirstLevel_Time;
        }

        private void SetNextLevel()
        {
            CurrentLevel++;
            CurrentLevelTime += Settings.Level_TimeDelta;
            ElapsedLevelTime = new TimeSpan();
        }

        public void Update(GameTime gameTime)
        {
            ElapsedLevelTime = ElapsedLevelTime.Add(gameTime.ElapsedGameTime);
            ElapsedBlockSpawned = ElapsedBlockSpawned.Add(gameTime.ElapsedGameTime);

            if(ElapsedLevelTime.Seconds > CurrentLevelTime)
                SetNextLevel();
        }

        #region Properties

        public int CurrentLevel
        {
            get { return currentLevel; }
            protected set { currentLevel = value; }
        }

        public double CurrentLevelTime
        {
            get { return currentLevelTime; }
            protected set { currentLevelTime = value; }
        }

        public TimeSpan ElapsedLevelTime
        {
            get { return elapsedLevelTime; }
            protected set { elapsedLevelTime = value; }
        }

        public TimeSpan ElapsedBlockSpawned
        {
            get { return elapsedBlockSpawned; }
            set { elapsedBlockSpawned = value; }
        }

        #endregion
    }
}