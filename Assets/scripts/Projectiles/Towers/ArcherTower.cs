using System;
using Game.Resources.Items;
using Projectiles.Mobs;
using UnityEngine;

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
            Cooldown = 0.5f;
            MinCooldown = 0.1f;
            AmmoType = AmmoType.Bullet;
        }
    }
}