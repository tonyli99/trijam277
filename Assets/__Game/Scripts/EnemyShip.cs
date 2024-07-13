using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class EnemyShip : Ship
    {

        [field: SerializeField] private SalvageTrigger SalvagePrefab;

        public ItemSO Item { get; set; }

        private Transform myTransform;
        private Transform playerTransform;
        private bool hasDestination = false;
        private Vector3 destination;

        private const float AttackDistance = 15;
        private const float FiringDistance = 4;
        private const float MaxSpeed = 50;

        private void Start()
        {
            myTransform = transform;
            playerTransform = FindFirstObjectByType<PlayerShipController>().transform;
        }

        private void Update()
        {
            if (playerTransform == null || playerTransform.gameObject == null) return;
            var distanceToPlayer = Vector3.Distance(myTransform.position, playerTransform.position);
            if (hasDestination)
            {
                var angle = Mathf.Atan2(destination.y - myTransform.position.y, destination.x - myTransform.position.x) * Mathf.Rad2Deg;
                var targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
                myTransform.rotation = Quaternion.RotateTowards(myTransform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
                if (RB.velocity.magnitude < MaxSpeed) RB.AddForce(myTransform.up * ThrustForce * Time.deltaTime);
            }   
            else if (distanceToPlayer < AttackDistance)
            {
                var direction = (playerTransform.position - myTransform.position).normalized;
                destination = myTransform.position + (2 + distanceToPlayer) * direction;
                hasDestination = true;
            }
            else
            {
                RB.velocity = Vector3.zero;
            }
            if (distanceToPlayer < FiringDistance)
            {
                TryFire();
            }
        }

        protected override void OnDied()
        {
            base.OnDied();
            var salvage = Instantiate(SalvagePrefab, transform.position, Quaternion.identity);
            salvage.Item = Item;
            salvage.Quantity = Random.Range(1, 20);
            Destroy(gameObject);
        }
    }

}
