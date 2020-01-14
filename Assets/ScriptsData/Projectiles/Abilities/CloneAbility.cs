using UnityEngine;

namespace Projectiles.Abilities
{
    public class CloneAbility : Ability
    {
        public int CloneRate;

        private void Start()
        {
            AbilityType = AbilityType.Clone;
            UseAbility();
        }
    }
}