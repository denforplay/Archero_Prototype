using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Weapon configuration")]
    public class WeaponConfiguration : ScriptableObject
    {
        [SerializeField][Range(0.5f, 5f)] private float _fireRate;
        [SerializeField][Range(0.5f, 10f)] private float _shootSpeed;
        [SerializeField] [Range(0.5f, 10f)] private float _bulletLifeTime;

        public float ShootSpeed => _shootSpeed;
        public float FireRate => _fireRate;
        public float BulletLifeTime => _bulletLifeTime;
    }
}