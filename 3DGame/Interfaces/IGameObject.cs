using Microsoft.Xna.Framework;

namespace _3DGame
{
    public interface IGameObject
    {
        Matrix WorldMatrix { get; }
    }
}