using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace _3DGame
{
    public static class Settings
    {
        public static Vector3 StartingPlayerPosition = Vector3.Zero;
        public static float XOffset = -10f;
        public static float YOffset = 4.5f;

        //Model Scales
        public static float Character_Scale = 0.1f;
        public static float Box_Scale = /*0.0005f*/ 1f;

        //Character Bounding Sphere Scales
        public static float CharacterBoundingSphere_XOffset = 0f;
        public static float CharacterBoundingSphere_YOffset = -0.4f;
        public static float CharacterBoundingSphere_ZOffset = -0.3f;
        public static float CharacterBoundingSphere_Scale = 0.5f;

        //Box Bounding Sphere Scales
        public static float BoxBoundingSphere_Scale = 0.08f;


        //Coords
        public static float CharacterBoxSpawning_Distance = 10f;
        public static float BoxSpawning_Max_ZOffset = 100f;
        public static float BoxSpawning_Max_XOffset = 100f;
        public static float BoxSpawning_Max_YOffset = 30f;
        public static float BoxSpawning_Min_ZOffset = 70f;
        public static float BoxSpawning_Min_XOffset = 0f;
        public static float BoxSpawning_Min_YOffset = 0f;


        //Moving Settings
        public static float Box_Velocity = 0.05f;
        public static float Box_Max_Speed = 1f;
        public static float Box_Living_Time = 5f;


        //Player Constants
        public static byte Character_Max_Heath = 3;

    }

}
