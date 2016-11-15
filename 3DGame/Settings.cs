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
        public static float CharacterScale = 0.1f;

        public static float CharacterBoundingSphereScale = CharacterScale + CharacterScale * 10;
        public static float BoxBoundingSphereScale = 1f;
    }
}
