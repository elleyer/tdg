using System;
using UnityEngine;

namespace Projectiles.Mobs
{
    public class Ufo : Enemy
    {
        private void Start()
        {
            EnemyType = EnemyType.Air;
            Health = 100;
            Speed = 1.5f;
        }
    }
}