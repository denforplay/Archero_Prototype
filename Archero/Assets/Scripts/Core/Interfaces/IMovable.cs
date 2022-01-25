using UnityEngine;

namespace Core.Interfaces
{
    public interface IMovable
    {
        Vector2 Direction { get; set; }
        void Move(Vector3 delta);
    }
}