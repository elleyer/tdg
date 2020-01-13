using System;
using System.Collections;
using Audio;
using Projectiles.Mobs;
using UnityEngine;
using Game.Resources;
using Game.Resources.Items;

namespace Projectiles.Towers
{
    public class Defender : MonoBehaviour //Basic defender class that all other types derives from. Probably we need to use interface here
    {
        public float Damage, MaxDamage;
        public int DamageLevel;
        public int CooldownLevel;
        public float MinRadius, MaxRadius;
        //public float BulletSpeed, MaxBulletSpeed;
        public float Cooldown, MinCooldown;
        public bool Attacking;
        public AmmoType AmmoType;
        public EnemyType EnemyType;

        public void StartAttack(Enemy enemy)
        {
            StopAllCoroutines();
            Attacking = true;
            StartCoroutine(Attack(enemy));
        }

        private void Awake()
        {
            DamageLevel = CooldownLevel = 1;
        }

        private IEnumerator Attack(Enemy enemy)
        {
            while (true)
            {
                if (enemy != null)
                {
                    if (Attacking && Vector2.Distance(gameObject.transform.position, enemy.transform.position) <=
                        MaxRadius && (EnemyType == enemy.EnemyType || EnemyType == EnemyType.Both))
                    {
                        GameObject weaponInstance;
                        switch (AmmoType)
                        {
                            case AmmoType.Bullet:
                                AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool
                                    .BlasterShot);
                                weaponInstance = Instantiate(ResourcesProvider.Instance.ObjectPool.Bullet,
                                    gameObject.transform.position, Quaternion.identity);
                                break;
                            case AmmoType.Rocket:
                                AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool
                                    .RocketShot);
                                weaponInstance = Instantiate(ResourcesProvider.Instance.ObjectPool.Rocket,
                                    gameObject.transform.position, Quaternion.identity);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        var bullet = weaponInstance.GetComponent<Bullet>();
                        bullet.Type = AmmoType;
                        bullet.Owner = this;
                        StartCoroutine(BulletAssist.MoveToEnemy(bullet, enemy, AmmoType, this));
                    }
                    else Attacking = false;
                }
                else Attacking = false;
                yield return new WaitForSeconds(Cooldown);
            }
        }
    }

    public enum DefenderType //Enum types
    {
        ArcherTower,
        Cannon,
        Mortar
    }
}
