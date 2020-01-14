using UnityEngine;

namespace Projectiles.Abilities
{
    public class Ability : MonoBehaviour //Base ability class
    {
        public float AbilityTime;
        public AbilityType AbilityType;

        public void UseAbility()
        {

        }
    }

    public enum AbilityType
    {
        Damage,
        Clone,
        Speed
    }
}