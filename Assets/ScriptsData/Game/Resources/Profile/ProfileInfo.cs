using UnityEngine;

namespace Game.Resources.Profile
{
    public class ProfileInfo : MonoBehaviour
    {
        public static ProfileInfo Instance = null;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public Wallet Wallet;
        public Statistics Statistics;
    }
}