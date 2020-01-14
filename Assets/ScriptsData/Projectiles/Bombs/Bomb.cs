using System.Linq;
using Audio;
using Game.Resources;
using Projectiles.Mobs;
using UnityEngine;

namespace Projectiles.Bombs
{
    public class Bomb : MonoBehaviour
    {
        private float _radius = 3f;
        private float _damage = 20f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<Enemy>() == null)
                return;
            Explode();
            Destroy(gameObject);
        }

        private void Explode()
        {
            Debug.Log($"Explode with {_damage}");
            AudioProvider.Instance.AudioSource.PlayOneShot(AudioProvider.Instance.AudioPool.Explosion);
            foreach (var enemy in ResourcesProvider.Instance.Pool.Enemies.Where(x=> Vector2.Distance(x.transform.position, transform.position) <= _radius))
            {
                enemy.Hit(_damage / Vector2.Distance(gameObject.transform.position, enemy.transform.position));
            }
        }

        public enum BombTypes
        {
            C4,
            Mine
        }
    }
}