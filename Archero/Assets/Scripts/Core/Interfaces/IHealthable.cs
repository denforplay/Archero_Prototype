using System;

namespace Core.Interfaces
{
    public interface IHealthable
    {
        event Action<int> OnHealthChanged;
        int MaxHealthPoint { get; set; }
        int CurrentHealthPoints { get; set; }
        void TakeDamage(int damage);
    }
}