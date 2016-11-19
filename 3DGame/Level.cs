using System;
using Microsoft.Xna.Framework;

namespace _3DGame
{
    public class Level
    {
        private int currentLevel;
        private double currentLevelTime;
        private TimeSpan elapsedLevelTime;
        private TimeSpan elapsedBoxSpawned;
        private bool boxSpawned;
        private int boxSpawnedNumber;

        public Level()
        {
            CurrentLevel = 1;
            CurrentLevelTime = Settings.FirstLevel_Time;
            BoxSpawned = false;
            ElapsedBoxSpawned = new TimeSpan();
        }

        public void SetNextLevel()
        {
            CurrentLevel++;
            CurrentLevelTime += Settings.Level_TimeDelta;
            ElapsedLevelTime = new TimeSpan();
            BoxSpawned = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!BoxSpawned && boxSpawnedNumber >= BoxNumberToSpawn)
                BoxSpawned = true;
            ElapsedLevelTime = ElapsedLevelTime.Add(gameTime.ElapsedGameTime);

            if (BoxSpawned)
                ElapsedBoxSpawned = ElapsedBoxSpawned.Add(gameTime.ElapsedGameTime);

            if (ElapsedBoxSpawned.Seconds >= Settings.BoxSpawning_Interval)
                BoxSpawned = false;

            if (ElapsedLevelTime.Seconds > CurrentLevelTime)
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

        public TimeSpan ElapsedBoxSpawned
        {
            get { return elapsedBoxSpawned; }
            set { elapsedBoxSpawned = value; }
        }

        public bool BoxSpawned
        {
            get { return boxSpawned; }
            set
            {
                if(value)
                    ElapsedBoxSpawned = new TimeSpan();
                if (!value)
                {
                    BoxSpawnedNumber = 0;
                    ElapsedBoxSpawned = new TimeSpan();
                }
                boxSpawned = value;
            }
        }

        public int BoxSpawnedNumber
        {
            get { return boxSpawnedNumber; }
            set
            {
                
                boxSpawnedNumber = value;
                if (value >= BoxNumberToSpawn)
                    BoxSpawned = true;
            }
        }

        public int BoxNumberToSpawn => CurrentLevel;

        #endregion
    }
}