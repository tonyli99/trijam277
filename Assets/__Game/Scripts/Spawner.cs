using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Spawner : MonoBehaviour
    {

        [field: SerializeField] private Vector2 Bounds;
        [field: SerializeField] private EnemyShip EnemyShipPrefab;
        [field: SerializeField] private GameObject PlanetPrefab;
        [field: SerializeField] private GameObject SalvagePrefab;

        private void Start()
        {
            for (int i = 0; i < 6; i++)
            {
                var pos = new Vector3(Random.value * Bounds.x, Random.value * Bounds.y);
                if (Random.value < 0.5) pos.x *= -1;
                if (Random.value < 0.5) pos.y *= -1;
                Instantiate(PlanetPrefab, pos, Quaternion.identity);
            }

            for (int i = 0; i < 50; i++)
            {
                var pos = new Vector3(Random.value * Bounds.x + 8, Random.value * Bounds.y + 8);
                if (Random.value < 0.5) pos.x *= -1;
                if (Random.value < 0.5) pos.y *= -1;
                Instantiate(EnemyShipPrefab, pos, Quaternion.identity);
            }

            for (int i = 0; i < 30; i++)
            {
                var pos = new Vector3(Random.value * Bounds.x, Random.value * Bounds.y);
                if (Random.value < 0.5) pos.x *= -1;
                if (Random.value < 0.5) pos.y *= -1;
                Instantiate(SalvagePrefab, pos, Quaternion.identity);
            }
        }

    }

}
