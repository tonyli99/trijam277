using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{

    public class CommerceUI : MonoBehaviour
    {
        [field: SerializeField] public GameObject Window;
        [field: SerializeField] public TextMeshProUGUI PlanetNameTmp;
        [field: SerializeField] public Button LeaveButton;

        public static CommerceUI Instance { get; private set; }

        private Canvas canvas;
        private PlanetTrigger planet;

        private void Awake()
        {
            Instance = this;
            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
            Window.SetActive(false);
        }

        private void Start()
        {
            LeaveButton.onClick.AddListener(() => planet.Leave());
        }

        public void Open(PlanetTrigger planet)
        {
            canvas.enabled = true;
            Window.SetActive(true);
            this.planet = planet;
            PlanetNameTmp.text = planet.PlanetName;
        }

        public void Close()
        {
            canvas.enabled = false;
            Window.SetActive(false);
        }

    }
}
