using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Weapon configuration")]
    public class WeaponConfiguration : ScriptableObject
    {
        [SerializeField][Range(0.5f, 5f)] private float _fireRate;
        [SerializeField][Range(0.5f, 10f)] private float _shootSpeed;
        [SerializeField] private float _bulletLifeTime;
        [SerializeField] [Range(0, 500)] private int _bulletDamage;

        public float ShootSpeed => _shootSpeed;
        public float FireRate => _fireRate;
        public float BulletLifeTime => _bulletLifeTime;
        public int BulletDamage => _bulletDamage;
    }
}