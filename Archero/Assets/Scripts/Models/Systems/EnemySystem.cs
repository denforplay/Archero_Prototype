using Core;
using Models.Enemies;

namespace Models.Systems
{
    public class EnemySystem : System<EnemyBase>
    {
        public void Work(EnemyBase enemy)
        {
            Entity<EnemyBase> enemyEntity = new Entity<EnemyBase>(enemy, enemy);
            Work(enemyEntity);
        }

       
        
        public override void UpdateSystem(float deltaTime)
        {
            foreach (var entity in Entities)
            {
                //(entity.GetEntity as IMovable)?.Move();
            }
        }
    }
}