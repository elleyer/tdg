using System.Collections;
using System.Linq;
using Game.Resources;
using Game.Wave;
using Projectiles.Mobs;
using Projectiles.Towers;
using UnityEngine;

namespace AI.EnemySelector
{
    public class EnemySelectorHandler : MonoBehaviour
    {
        private Defender _defender;
        private Enemy _enemy;

        public void Start()
        {
            _defender = GetComponent<Defender>();
            StartCoroutine(SelectEnemy()); //debug feature
        }

        private IEnumerator SelectEnemy()
        {
            while (true)
            {
                if (!WavesHandler.Instance.PlayableWave)
                    yield return new WaitUntil(() => WavesHandler.Instance.PlayableWave);
                var comparer = Mathf.Infinity;
                foreach (var enemy in ResourcesProvider.Instance.Pool.Enemies.ToList().Where(enemy =>
                    Vector2.Distance(enemy.transform.position, gameObject.transform.position) < comparer
                                      && (_defender.EnemyType == enemy.EnemyType || _defender.EnemyType == EnemyType.Both)))
                {
                    comparer = Vector2.Distance(enemy.transform.position, gameObject.transform.position);
                    _enemy = enemy;
                    if (comparer < _defender.MaxRadius && _enemy.Health > 0)
                    {
                        if (!_defender.Attacking)
                            _defender.StartAttack(_enemy);
                    }
                }
                yield return new WaitForSeconds(1);
            }
        }
    }
}