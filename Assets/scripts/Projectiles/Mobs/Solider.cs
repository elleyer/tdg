namespace Projectiles.Mobs
{
    public class Solider : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Ground;
            Health = 100;
            Speed = 1;
            Reward = 100;
        }
    }
}