using System;
using UnityEngine;

namespace Projectiles.Towers
{
    public class ArcherTower : Defender //ArcherTower class with it's own params
    {
        public void Start()
        {
            Damage = 10;
            MaxDamage = 60;
            MinRadius = 0;
            MaxRadius = 7;
            Cooldown = 0.5f;
            MinCooldown = 0.2f;
        }
    }
}