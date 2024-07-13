using System.Collections;
using UnityEngine;
using TMPro;

namespace Game
{

    public class MessageUI : MonoBehaviour
    {

        [field: SerializeField] private Transform container;
        [field: SerializeField] private TextMeshProUGUI messageTemplate;

        public static MessageUI Instance { get; private set; }

        private WaitForSecondsRealtime messageDelay = new WaitForSecondsRealtime(5);

        private void Awake()
        {
            Instance = this;
            messageTemplate.gameObject.SetActive(false);
        }

        public void AddMessage(string text)
        {
            StartCoroutine(MessageCoroutine(text));
        }

        private IEnumerator MessageCoroutine(string text)
        {
            var message = Instantiate(messageTemplate, container);
            message.gameObject.SetActive(true);
            message.text = text;
            yield return messageDelay;
            Destroy(message.gameObject);
        }

    }
}
