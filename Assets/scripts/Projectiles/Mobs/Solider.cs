namespace ScriptsData.Projectiles.Mobs
{
    public class Solider : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Ground;
            Health = 100;
            Speed = 1;
        }
    }
}