using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class SalvageTrigger : TriggerEvent
    {

        [field: SerializeField] public ItemSO Item { get; set; }
        [field: SerializeField] public int Quantity { get; set; }

        private Transform myTransform;
        private float rotationSpeed;

        private void Start()
        {
            myTransform = transform;
            rotationSpeed = Random.Range(0.1f, 1f);
            OnTriggerEnter.AddListener(() =>
            {
                AudioManager.Instance.Pickup();
                MessageUI.Instance.AddMessage($"Salvaged {Quantity} {Item.Name}");
                Inventory.Instance.Add(Item, Quantity);                
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            myTransform.Rotate(new Vector3(0, 0, 360 * rotationSpeed * Time.deltaTime));
        }

    }

}
