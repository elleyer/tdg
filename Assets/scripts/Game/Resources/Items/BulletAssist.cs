using System.Collections;
using DG.Tweening;
using Projectiles.Mobs;
using Projectiles.Towers;
using UnityEngine;

namespace Game.Resources.Items
{
    public class BulletAssist : MonoBehaviour
    {
        public static IEnumerator MoveToEnemy(Bullet bullet, Enemy enemy, AmmoType type, Defender owner)
        {
            if (bullet == null)
                yield break;

            if (enemy == null)
            {
                owner.Attacking = false;
                Debug.Log("We are attacking. Again.");
                yield break;
            }

            var enPosition = enemy.transform.position;
            switch (type)
            {
                case AmmoType.Rocket:
                    while (bullet != null)
                    {
                        bullet.transform.DOMove(enPosition, 2.3f).From(bullet.transform.position);
                        bullet.transform.right = enPosition - bullet.transform.position;
                        if(enemy != null)
                           enPosition = enemy.transform.position;
                        yield return new WaitForFixedUpdate();
                    }
                    break;
                case AmmoType.Bullet:
                    var position = enemy.transform.position;
                    bullet.transform.DOMove(position, 0.4f).From(bullet.transform.position);
                    bullet.transform.right = position - bullet.transform.position;
                    break;
            }
        }
    }
}