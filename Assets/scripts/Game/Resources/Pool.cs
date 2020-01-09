using System.Collections.Generic;
using ScriptsData.Game.Wave;
using ScriptsData.Projectiles.Mobs;
using ScriptsData.Projectiles.Towers;
using UnityEngine;

namespace ScriptsData.Game.Resources
{
    public class Pool : MonoBehaviour
    {
        public List<Defender> Defenders;
        public List<Enemy> Enemies;
        public event WavesHandler.DestroyedHandler AllEnemiesDestroyed;
        internal void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            Debug.Log(enemy.name);
            enemy.Destroyed += OnEnemyDestroyed;
        }

        internal void AddDefender(Defender defender) => Defenders.Add(defender);

        private void OnEnemyDestroyed(Enemy enemy)
        {
            Debug.Log($"{enemy.Health} destroyed");
            Destroy(enemy.gameObject);
            RemoveFromPool(enemy);
        }

        private void RemoveFromPool(Enemy enemy)
        {
            if(Enemies.Contains(enemy))
                Enemies.Remove(enemy);

            if(Enemies.Count <=0)
                AllEnemiesDestroyed?.Invoke();
        }

        private void RemoveFromPool(Defender defender)
        {

        }
    }
}