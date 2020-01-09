using System;
using System.Collections;
using System.Collections.Generic;
using ScriptsData.Game.Resources;
using ScriptsData.Projectiles.Mobs;
using UnityEngine;

namespace ScriptsData.Game.Wave
{
    public class WavesHandler : MonoBehaviour //Wave controller that should check currently game state
    {
        public int CurrentWave;
        public Wave Wave;
        private ResourcesProvider _resourcesProvider; //ref to rhe resources provider
        private Pool _pool; //Pool with all active objects
        [SerializeField] private GameObject _soliderPrefab, _ufoPrefab;
        [SerializeField] private Transform _parent;
        private List<EnemyName> _enemyNames;

        //Event for handling active and destroyed enemies
        public delegate void DestroyedHandler();
        public delegate void EnemyHandler(Enemy enemy);

        private void Start()
        {
            _resourcesProvider = GetComponent<ResourcesProvider>();
            _pool = _resourcesProvider.Pool;
            _pool.AllEnemiesDestroyed += OnAllEnemiesDestroyed; //Subscribe on method that handle this behaviour
        }

        public void NextWave() //Add smth like "Battle result" as arguments
        {
            CurrentWave++;
            Wave = new Wave(10 * CurrentWave, 20, 1000 * CurrentWave);
            _enemyNames = Wave.GetEnemies();
            StartCoroutine(SpawnEnemies(Wave.EnemyCount, 1f));
            Debug.Log($"{Wave.EnemyCount} {Wave.AirPercentage} {Wave.Reward}");
        }

        private IEnumerator SpawnEnemies(int enemyCount, float timeDelay) //Spawn enemies with given delay
        {
            for (var i = 0; i < enemyCount; i++)
            {
                GameObject enemyName;

                switch (_enemyNames[i])
                {
                    case EnemyName.Solider:
                        enemyName = _soliderPrefab;
                        break;
                    case EnemyName.Ufo:
                        enemyName = _ufoPrefab;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                var enemy = Instantiate(enemyName, _parent.transform).GetComponent<Enemy>();
                enemy.SetPath(_resourcesProvider.PathCreator.Nodes); //push nodes to enemy path
                _pool.AddEnemy(enemy); //Add enemy to the pool
                yield return new WaitForSeconds(timeDelay); //Wait for delay
            }
            StopCoroutine($"SpawnEnemies");
        }

        private void OnAllEnemiesDestroyed() => NextWave(); //Call next wave if all enemies are destroyed
    }
}