using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/HeroConfiguration")]
    public class HeroConfiguration : ScriptableObject
    {
        [SerializeField][Range(0.01f, 1f)]
        private float _speed;
        public float Speed => _speed;
    }
}