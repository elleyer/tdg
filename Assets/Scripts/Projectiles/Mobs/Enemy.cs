﻿using System.Collections;
using System.Collections.Generic;
using Game;
using Game.Wave;
using UnityEngine;
using UnityEngine.UI;
using Utils.Navigation;

namespace Projectiles.Mobs
{
    public class Enemy : MonoBehaviour //Base class for all the Enemies
    {
        public Image HealthFill;
        public float Health = 100;
        public float CoolDown;
        public float Speed;
        public float Damage;
        public EnemyType EnemyType;
        public int Reward;
        private readonly List<Transform> _pathPoints = new List<Transform>();

        public event WavesHandler.EnemyHandler Destroyed;

        internal void SetPath(IEnumerable<Node> nodes) //We should say to enemy how to go to the end. Push list of nodes as data
        {
            foreach (var data in nodes)
            {
                _pathPoints.Add(data.transform);
            }

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            foreach (var t in _pathPoints)
            {
                while (transform.position != t.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, t.transform.position, Speed / 64f);
                    yield return new WaitForFixedUpdate();
                }
            }
        }

        public void Update()
        {
            if (Health <= 0)
            {
                Destroyed?.Invoke(this);
            }
        }

        public void Hit(float value)
        {
            Health -= value;
            HealthFill.fillAmount = 1f / 100f * Health;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<Tower>() != null)
                StartCoroutine(HitTower(other.GetComponent<Tower>()));
        }

        private IEnumerator HitTower(Tower tower)
        {
            while (tower != null)
            {
                tower.Hit((int)Damage);
                yield return new WaitForSeconds(CoolDown);
            }
        }
    }


    public enum EnemyType
    {
        Ground,
        Air,
        Both
    }

    public enum EnemyName
    {
        Solider,
        Ufo
    }
}

