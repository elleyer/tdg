using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using Projectiles.Mobs;
using Projectiles.Towers;
using UnityEngine;

namespace Game.Resources.Items
{
    public class Bullet : MonoBehaviour
    {
        public float Damage, Speed;
        public AmmoType Type;
        public Defender Owner;

        private void Start()
        {
            Destroy(gameObject, 3);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.GetComponent<Enemy>() == null)
                return;

            var enemy = other.gameObject.GetComponent<Enemy>();
            switch (Type)
            {
                case AmmoType.Bullet:
                    enemy.Hit(Damage);
                    break;
                case AmmoType.Rocket:
                    var nearestEnemies = ResourcesProvider.Instance.Pool.Enemies.Where
                        (x => Vector2.Distance(gameObject.transform.position, x.transform.position) <= 2);
                    foreach (var nEnemy in nearestEnemies)
                    {
                        nEnemy.Hit(Damage / Vector2.Distance(gameObject.transform.position, nEnemy.transform.position));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    public enum AmmoType
    {
        Bullet,
        Rocket
    }
}