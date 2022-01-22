using UnityEngine;

namespace Configurations
{
    [CreateAssetMenu(menuName = "Configurations/Weapon configuration")]
    public class WeaponConfiguration : ScriptableObject
    {
        [SerializeField] private float _fireRate;

        public float FireRate => _fireRate;
    }
}