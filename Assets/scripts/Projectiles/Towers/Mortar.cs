using Game.Resources.Items;
using UnityEngine;

namespace Projectiles.Towers
{
    public class Mortar : Defender //Mortar class with it's own params
    {
        public void Start()
        {
            Damage = 10;
            MaxDamage = 50;
            MinRadius = 1;
            MaxRadius = 3;
            Cooldown = 1f;
            MinCooldown = 0.5f;
            AmmoType = AmmoType.Rocket;
        }
    }
}