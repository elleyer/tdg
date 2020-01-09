using UnityEngine;

namespace Projectiles.Towers
{
    public class Mortar : Defender //Mortar class with it's own params
    {
        public void Start()
        {
            Damage = 10;
            MinRadius = 0.64f * 3;
            MaxRadius = 0.64f * 5;
        }
    }
}