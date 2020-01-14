namespace Projectiles.Mobs
{
    public class Ufo : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Air;
            Damage = 200;
            CoolDown = 1;
            Health = 100;
            Speed = 2.5f;
            Reward = 150;
        }
    }
}