using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Abstracts
{
    public abstract class Transformable
    {
        public Vector2 Speed { get; set; }
        public virtual Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public Transformable(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }
    }
}