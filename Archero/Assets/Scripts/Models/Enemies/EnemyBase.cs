using Core.Abstracts;
using UnityEngine;

namespace Models.Enemies
{
    public abstract class EnemyBase : Transformable
    {

        protected EnemyBase(Vector3 position, Vector3 rotation) : base(position, rotation)
        {
        }
        
        public abstract void Update(float deltaTime);
    }
}