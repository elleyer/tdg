using Game.Resources.Items;
using Projectiles.Mobs;
using UnityEngine;

namespace Projectiles.Towers
{
    public class Cannon : Defender //Cannon class with it's own params
    {
        public void Awake()
        {
            EnemyType = EnemyType.Ground;
            Damage = 10;
            MaxDamage = 70;
            MinRadius = 0;
            MaxRadius = 3;
            Cooldown = 1f;
            MinCooldown = 0.3f;
            AmmoType = AmmoType.Bullet;
        }
    }
}