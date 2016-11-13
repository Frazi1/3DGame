using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;

namespace _3DGame
{
    public interface ICollidable
    {
        BoundingBox BoundingBox { get; set; }
        //void UpdateBoundingBox();
    }
}
