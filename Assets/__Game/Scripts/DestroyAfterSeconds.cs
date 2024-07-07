using UnityEngine;

namespace Game
{

    public class DestroyAfterSeconds : MonoBehaviour
    {

        [field: SerializeField] private float Seconds = 1;

        private void Start()
        {
            Invoke(nameof(DestroyMe), Seconds);
        }

        private void DestroyMe()
        {
            Destroy(gameObject);
        }
    }

}
