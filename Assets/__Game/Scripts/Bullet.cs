using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Bullet : MonoBehaviour
    {

        [field: SerializeField] public float Force { get; set; } = 100;
        [field: SerializeField] public float LifetimeSeconds { get; set; } = 5;

        public System.Action<Bullet> Despawn = null;

        private GameObject firer;
        private int damage;

        public void Fire(GameObject firer, Transform muzzle, int damage)
        {
            this.firer = firer;
            this.damage = damage;
            var rb = GetComponent<Rigidbody2D>();
            transform.SetPositionAndRotation(muzzle.position, muzzle.rotation);
            rb.AddForce(transform.up * Force);
            Invoke(nameof(Expire), LifetimeSeconds);
        }

        private void Expire()
        {
            Despawn?.Invoke(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == firer) return;
            if (collision.CompareTag(Tags.Enemy) ||collision.CompareTag(Tags.Player))
            {
                var ship = collision.GetComponent<Ship>();
                if (ship != null)
                { 
                    ship.TakeDamage(damage);
                }
                Despawn?.Invoke(this);
            }
        }

    }

}
