using System.Collections.Generic;
using Game.Resources.Profile;
using Game.Wave;
using Projectiles.Mobs;
using Projectiles.Towers;
using UnityEngine;

namespace Game.Resources
{
    public class Pool : MonoBehaviour
    {
        public List<Defender> Defenders { get; } = new List<Defender>();
        public List<Enemy> Enemies { get; } = new List<Enemy>();

        public event WavesHandler.DestroyedHandler AllEnemiesDestroyed;
        internal void AddEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            enemy.Destroyed += OnEnemyDestroyed;
            enemy.Destroyed += ProfileInfo.Instance.Wallet.AddBalanceByEnemy;
        }

        internal void AddDefender(Defender defender) => Defenders.Add(defender);

        private void OnEnemyDestroyed(Enemy enemy)
        {
            ProfileInfo.Instance.Statistics.EnemiesDestroyed++;
            RemoveFromPool(enemy);
            Destroy(enemy.gameObject);
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
            if(Defenders.Contains(defender))
                Defenders.Remove(defender);
        }
    }
}