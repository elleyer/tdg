using UnityEngine;

namespace Projectiles.Abilities
{
    public class DamageAbility : Ability
    {
        public float AbilityDamage;

        private void Start()
        {
            AbilityType = AbilityType.Damage;
            UseAbility();
        }
    }
}