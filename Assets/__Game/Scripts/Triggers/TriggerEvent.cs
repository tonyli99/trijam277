using UnityEngine;
using UnityEngine.Events;

namespace Game
{

    public class TriggerEvent : MonoBehaviour
    {

        [field: SerializeField] public UnityEvent OnTriggerEnter { get; private set; } = new UnityEvent();
        [field: SerializeField] public UnityEvent OnTriggerExit { get; private set; } = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                OnTriggerEnter.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                OnTriggerExit.Invoke();
            }
        }

    }

}
