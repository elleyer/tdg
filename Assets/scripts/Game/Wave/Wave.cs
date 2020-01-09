using System.Collections.Generic;
using Projectiles.Mobs;
using UnityEngine;

namespace Game.Wave
{
    public class Wave
    {
        public int EnemyCount { get; }
        public int AirPercentage { get; }
        public int Reward { get; }

        public Wave(int enemyCount, int airPercentage, int reward)
        {
            EnemyCount = enemyCount;
            AirPercentage = airPercentage;
            Reward = reward;
        }

        public List<EnemyName> GetEnemies()
        {
            var list = new List<EnemyName>();
            var airCount = Mathf.RoundToInt(EnemyCount / 100f * AirPercentage);
            var groundCount = EnemyCount - airCount;
            Debug.Log($"{airCount} {groundCount}");

            for (var i = 0; i < airCount; i++)
            {
                Debug.Log("ufo++");
                list.Add(EnemyName.Ufo);
            }
            for (var i = 0; i < groundCount; i++)
            {
                list.Add(EnemyName.Solider);
            }

            return list;
        }
    }
}