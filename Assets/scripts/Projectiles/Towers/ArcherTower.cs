using System;
using UnityEngine;

namespace Projectiles.Towers
{
    public class ArcherTower : Defender //ArcherTower class with it's own params
    {
        public void Start()
        {
            Damage = 10;
            MinRadius = 0;
            MaxRadius = 0.64f * 7;
        }
    }
}