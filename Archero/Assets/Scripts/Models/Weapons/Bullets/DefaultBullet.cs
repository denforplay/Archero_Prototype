using UnityEngine;

namespace Models.Weapons.Bullets
{
    public class DefaultBullet : Bullet
    {
        public DefaultBullet(Vector3 position, Vector3 rotation, float lifeTime, float speed) : base(position, rotation, 5f, 2f)
        {
        }
    }
}