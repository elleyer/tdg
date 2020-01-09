using System;
using UnityEngine;

namespace Projectiles.Towers
{
    public class Defender : MonoBehaviour //Basic defender class that all other types derives from.
    {
        public int Damage;
        public float MinRadius, MaxRadius;

        public void Update()
        {

        }
    }

    public enum DefenderType //Enum types
    {
        ArcherTower,
        Cannon,
        Mortar
    }
}
