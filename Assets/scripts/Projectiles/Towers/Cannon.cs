using UnityEngine;

namespace ScriptsData.Projectiles.Towers
{
    public class Cannon : Defender //Cannon class with it's own params
    {
        public void Start()
        {
            Damage = 10;
            MinRadius = 0;
            MaxRadius = 0.64f * 5;
        }

        private void Update()
        {
            Debug.Log("Am shootin'");
        }
    }
}