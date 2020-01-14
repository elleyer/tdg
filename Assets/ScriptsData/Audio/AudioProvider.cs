using UnityEngine;

namespace Audio
{
    public class AudioProvider : MonoBehaviour
    {
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public static AudioProvider Instance;
        public AudioPool AudioPool;
        public AudioSource AudioSource;
    }
}