using UnityEngine;

namespace Models.Weapons.Bullets
{
    public class DefaultBullet : Bullet
    {
        public DefaultBullet(Vector3 position, Vector3 rotation, float lifeTime, Vector2 speed) : base(position, rotation, lifeTime, speed)
        {
        }
    }
}