using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Ship : MonoBehaviour
    {

        [field: SerializeField] public float RotationSpeed { get; set; } = 360f;
        [field: SerializeField] public float ThrustForce { get; set; } = 100f;
        [field: SerializeField] Transform MuzzlePosition { get; set; }
        [field: SerializeField] public Bullet BulletPrefab { get; set; }
        [field: SerializeField] public int BulletDamage { get; set; } = 10;
        [field: SerializeField] float BulletsPerSecond { get; set; } = 10f;
        [field: SerializeField] public int CurrentHealth { get; set; }
        [field: SerializeField] public int MaxHealth { get; set; }
        [field: SerializeField] public GameObject ExplosionPrefab { get; set; }

        protected Rigidbody2D RB { get; private set; }

        private Stack<Bullet> bulletPool { get; set; } = new Stack<Bullet>();
        private float timeNextFire;

        protected virtual void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
            if (CurrentHealth > 0)
            {
                OnHurt();
            }
            else
            {
                OnDied();
            }
        }

        private Bullet SpawnBullet()
        {
            if (bulletPool.Count > 0)
            {
                var instance = bulletPool.Pop();
                instance.gameObject.SetActive(true);
                return instance;
            }
            else
            {
                var instance = Instantiate(BulletPrefab);
                instance.Despawn += DespawnBullet;
                return instance;
            }
        }

        private void DespawnBullet(Bullet instance)
        {
            instance.gameObject.SetActive(false);
            bulletPool.Push(instance);
        }

        protected void TryFire()
        {
            if (Time.time >= timeNextFire)
            {
                timeNextFire = Time.time + (1 / BulletsPerSecond);
                Fire();
            }
        }

        private void Fire()
        {
            var bullet = SpawnBullet();
            bullet.Fire(gameObject, MuzzlePosition, BulletDamage);
            AudioManager.Instance.Shoot();
        }

        protected virtual void OnHurt()
        { }

        protected virtual void OnDied()
        {
            AudioManager.Instance.Explode();
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

}
