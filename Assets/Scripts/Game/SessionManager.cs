using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        public void Restart()
        {
            Initiate.Fade("SampleScene", Color.black, 1f);
        }
    }
}