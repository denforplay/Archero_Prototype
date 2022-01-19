using UnityEngine;

namespace Core.Abstracts
{
    public abstract class Transformable
    {
        public Vector2 Position { get; protected set; }
        public Vector3 Rotation { get; private set; }

        public Transformable(Vector2 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}