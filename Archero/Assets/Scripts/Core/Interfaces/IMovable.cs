using UnityEngine;

namespace Core.Interfaces
{
    public interface IMovable
    {
        Vector2 Speed { get; set; }
        void Move(Vector2 delta);
    }
}