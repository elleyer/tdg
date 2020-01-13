namespace Projectiles.Mobs
{
    public class Ufo : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Air;
            Health = 100;
            Speed = 2.5f;
            Reward = 150;
        }
    }
}