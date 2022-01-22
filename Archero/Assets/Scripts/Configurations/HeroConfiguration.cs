using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Hero configuration")]
    public class HeroConfiguration : ScriptableObject
    {
        [SerializeField][Range(0.01f, 1f)]
        private float _speed;
        public float Speed => _speed;
    }
}