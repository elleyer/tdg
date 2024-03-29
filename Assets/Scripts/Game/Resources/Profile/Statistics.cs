using UI;
using UnityEngine;

namespace Game.Resources.Profile
{
    public class Statistics : MonoBehaviour
    {
        private int _wavesPassed, _enemiesDestroyed, _defendersCreated, _enemiesAlive, _towerHealth;
        //We *Need* to use switch here.
        public void WavePassed()
        {
            _wavesPassed++;
            UserInterfaceContainer.Instance.InfoBlockData.UpdateWavesInfo(_wavesPassed);
        }

        public void UpdateTowerHealth(int value)
        {
            _towerHealth = value;
            UserInterfaceContainer.Instance.InfoBlockData.UpdateTowerHealth(_towerHealth);
        }

        public void EnemyCreated()
        {
            _enemiesAlive++;
            UserInterfaceContainer.Instance.InfoBlockData.UpdateAliveCount(_enemiesAlive);
        }

        public void EnemyDestroyed()
        {
            _enemiesDestroyed++;
            _enemiesAlive--;
            UserInterfaceContainer.Instance.InfoBlockData.UpdateDestroyedCount(_enemiesDestroyed);
            UserInterfaceContainer.Instance.InfoBlockData.UpdateAliveCount(_enemiesAlive);
        }

        public void DefenderCreated()
        {
            _defendersCreated++;
            UserInterfaceContainer.Instance.InfoBlockData.UpdateDefenderInfo(_defendersCreated);
        }
    }
}