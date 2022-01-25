using Core.Abstracts;
using UnityEngine;

namespace Models.Weapons.Bullets
{
    public class DefaultBullet : Bullet
    {
        public DefaultBullet(Transformable parent,  Vector3 position, Vector3 rotation, float lifeTime, Vector2 speed, int damage) : base(parent, position, rotation, lifeTime, speed, damage)
        {
        }
    }
}