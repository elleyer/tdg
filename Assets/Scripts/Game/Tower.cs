using Audio;
using Game.Resources.Profile;
using UnityEngine;
using Screen = UI.Window.Screen;

namespace Game
{
    public class Tower : MonoBehaviour
    {
        public int Health = 10000;
        private bool _notified;

        private void Start() => ProfileInfo.Instance.Statistics.UpdateTowerHealth(Health);
        public void Hit(int value)
        {
            Health -= value;
            ProfileInfo.Instance.Statistics.UpdateTowerHealth(Health);
            if (Health < 3000 && !_notified)
            {
                _notified = true;
                AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool.TowerLowHealth);
                Screen.Instance.Push(4);
            }

            if (!(Health <= 0))
                return;
            AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool.Death);
            Screen.Instance.Push(1);
            Destroy(gameObject);
        }
    }
}