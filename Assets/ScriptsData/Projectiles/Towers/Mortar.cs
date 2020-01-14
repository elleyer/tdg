using Game.Resources.Items;
using Projectiles.Mobs;

namespace Projectiles.Towers
{
    public class Mortar : Defender //Mortar class with it's own params
    {
        public void Awake()
        {
            EnemyType = EnemyType.Ground;
            Damage = 30;
            MaxDamage = 50;
            MinRadius = 1;
            MaxRadius = 3;
            Cooldown = 1f;
            MinCooldown = 0.5f;
            MaxCooldown = 1f;
            AmmoType = AmmoType.Rocket;
        }
    }
}