using UnityEngine;

namespace Projectiles.Abilities
{
    public class Ability : MonoBehaviour //Base ability class
    {
        public float AbilityDamage;
        public float AbilityTime;
        public AbilityType AbilityType;
    }

    public enum AbilityType
    {
        Damage,
        Clone,
        Speed
    }
}