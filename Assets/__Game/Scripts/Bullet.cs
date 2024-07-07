using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Bullet : MonoBehaviour
    {

        [field: SerializeField] public float Force { get; set; } = 100;
        [field: SerializeField] public int Damage { get; set; } = 10;
        [field: SerializeField] public float LifetimeSeconds { get; set; } = 2;
        [field: SerializeField] public bool IsEnemyBullet { get; set; }

        public System.Action<Bullet> Despawn = null;

        public void Fire(Transform muzzle)
        {
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
            if (collision.CompareTag(Tags.Enemy) ||
                (IsEnemyBullet && collision.CompareTag(Tags.Player)))
            {
                var ship = collision.GetComponent<Ship>();
                if (ship != null)
                { 
                    ship.TakeDamage(Damage);
                }
                Despawn?.Invoke(this);
            }
        }

    }

}
