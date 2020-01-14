namespace Projectiles.Mobs
{
    public class Solider : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Ground;
            Damage = 100;
            CoolDown = 0.5f;
            Health = 100;
            Speed = 1;
            Reward = 100;
        }
    }
}