using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Hero configuration")]
    public class HeroConfiguration : ScriptableObject
    {
        [SerializeField][Range(0.01f, 5f)]
        private float _speed;

        [SerializeField][Range(1, 1000)] private int _startHealthPoints; 
        public float Speed => _speed;

        public int StartHealthPoint => _startHealthPoints;
    }
}