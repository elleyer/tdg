using Game.Resources.Items;
using Projectiles.Mobs;

namespace Projectiles.Towers
{
    public class ArcherTower : Defender //ArcherTower class with it's own params
    {
        public void Awake()
        {
            EnemyType = EnemyType.Both;
            Damage = 10;
            MaxDamage = 60;
            MinRadius = 0;
            MaxRadius = 7;
            Cooldown = 1f;
            MinCooldown = 0.3f;
            MaxCooldown = 1f;
            AmmoType = AmmoType.Bullet;
        }
    }
}