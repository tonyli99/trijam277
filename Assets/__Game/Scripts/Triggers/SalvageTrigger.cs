using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class SalvageTrigger : TriggerEvent
    {

        //[field: SerializeField] public 

        private Transform myTransform;
        private float rotationSpeed;

        private void Start()
        {
            myTransform = transform;
            rotationSpeed = Random.Range(0.1f, 1f);
            OnTriggerEnter.AddListener(() =>
            {
                Destroy(gameObject);
            });
        }

        private void Update()
        {
            myTransform.Rotate(new Vector3(0, 0, 360 * rotationSpeed * Time.deltaTime));
        }

    }

}
