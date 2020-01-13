using UnityEngine;

namespace Projectiles.Abilities
{
    public class SpeedAbility : Ability
    {
        public float SpeedRate;

        private void Start()
        {
            AbilityType = AbilityType.Speed;
            UseAbility();
        }
    }
}