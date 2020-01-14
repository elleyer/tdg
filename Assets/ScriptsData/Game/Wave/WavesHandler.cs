using System;
using System.Collections;
using System.Collections.Generic;
using AI.EnemySelector;
using Game.Resources;
using Game.Resources.Profile;
using UI.Window;
using Projectiles.Mobs;
using UnityEngine;
using Screen = UI.Window.Screen;

namespace Game.Wave
{
    public class WavesHandler : MonoBehaviour //Wave controller that should check currently game state
    {
        public static WavesHandler Instance;
        public int CurrentWave;
        public Wave Wave;
        private Pool _pool; //Pool with all active objects
        private List<EnemyName> _enemyNames;
        public bool PlayableWave;

        //Event for handling active and destroyed enemies
        public delegate void DestroyedHandler();
        public delegate void EnemyHandler(Enemy enemy);

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);
        }

        private void Start()
        {
            _pool = ResourcesProvider.Instance.Pool;
            _pool.AllEnemiesDestroyed += OnAllEnemiesDestroyed; //Subscribe on method that handle this behaviour
        }

        private void NextWave() //Add smth like "Battle result" as arguments
        {
            CurrentWave++;
            PlayableWave = true;
            Wave = new Wave(10 * CurrentWave, 20, 1000 * CurrentWave);
            _enemyNames = Wave.GetEnemies();
            StartCoroutine(SpawnEnemies(Wave.EnemyCount, 1f));
            StartCoroutine(UpdateList());
        }

        private void WavePassed()
        {
            ProfileInfo.Instance.Wallet.AddBalance((uint)(1000*CurrentWave));
            ProfileInfo.Instance.Statistics.WavePassed();
            Screen.Instance.Push(0);
        }

        private IEnumerator SpawnEnemies(int enemyCount, float timeDelay) //Spawn enemies with given delay
        {
            for (var i = 0; i < enemyCount; i++)
            {
                GameObject enemyName;
                ProfileInfo.Instance.Statistics.EnemyCreated();
                switch (_enemyNames[i])
                {
                    case EnemyName.Solider:
                        enemyName = ResourcesProvider.Instance.ObjectPool.Solider;
                        break;
                    case EnemyName.Ufo:
                        enemyName = ResourcesProvider.Instance.ObjectPool.Ufo;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                var enemy = Instantiate(enemyName, ResourcesProvider.Instance.ObjectPool.DefendersParent.transform).GetComponent<Enemy>();
                enemy.SetPath(ResourcesProvider.Instance.PathCreator.Nodes); //push nodes to enemy path
                _pool.AddEnemy(enemy); //Add enemy to the pool
                yield return new WaitForSeconds(timeDelay); //Wait for delay
            }
            StopCoroutine($"SpawnEnemies");
        }

        private IEnumerator UpdateList()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
            }
        }
        private void OnAllEnemiesDestroyed()
        {
            WavePassed();
            if (CurrentWave == 2)
            {
                Screen.Instance.Push(2);
                return;
            }
            NextWave();
        } //Call next wave if all enemies are destroyed
    }
}