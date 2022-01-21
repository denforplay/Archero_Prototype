using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Abstracts
{
    public abstract class Transformable
    {
        public Vector2 Speed { get; set; }
        public Vector3 Rotation { get; set; }
        public Transformable(Vector2 speed, Vector3 rotation)
        {
            Speed = speed;
            Rotation = rotation;
        }
    }
}