using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Enemy configuration")]
    public class EnemyConfiguration : ScriptableObject
    {
        [SerializeField][Range(1, 10)] 
        private int _minCount;
        [SerializeField][Range(1, 10)] 
        private int _maxCount;
        [SerializeField] [Range(1f, 10f)]
        private float _speed;
        [SerializeField] private int _startHealthPoints;

        public int MinCount => _minCount;
        public int MaxCount => _maxCount;
        public float Speed => _speed;
        public int StartHealthPoints => _startHealthPoints;
    }
}