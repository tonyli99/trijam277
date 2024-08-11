using System.Collections.Generic;
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

        [field: SerializeField] public TextMeshProUGUI Credits;

        [field: SerializeField] public Transform BuyContainer;
        [field: SerializeField] public ItemRow BuyRowTemplate;
        [field: SerializeField] public Transform SellContainer;
        [field: SerializeField] public ItemRow SellRowTemplate;

        public static CommerceUI Instance { get; private set; }

        private Canvas canvas;
        private PlanetTrigger planet;

        private List<GameObject> instances = new List<GameObject>();

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
            Refresh();
        }

        public void Close()
        {
            canvas.enabled = false;
            Window.SetActive(false);
        }

        private void Refresh()
        {
            Credits.text = Inventory.Instance.Credits.ToString();

            instances.ForEach(x => Destroy(x));
            instances.Clear();

            BuyRowTemplate.gameObject.SetActive(true);
            SellRowTemplate.gameObject.SetActive(true);

            foreach (var kvp in planet.Items)
            {
                var item = kvp.Key;
                var quantity = kvp.Value;
                var instance = Instantiate(BuyRowTemplate, BuyContainer);
                instances.Add(instance.gameObject);
                instance.Assign(item, quantity);
                instance.button.onClick.AddListener(() =>
                {
                    Buy(item, (int)instance.amountSlider.value);
                    Refresh();
                });
            }

            foreach (var kvp in Inventory.Instance.Items)
            {
                var item = kvp.Key;
                var quantity = kvp.Value;
                var instance = Instantiate(SellRowTemplate, SellContainer);
                instances.Add(instance.gameObject);
                instance.Assign(item, quantity);
                instance.button.onClick.AddListener(() =>
                {
                    Sell(item, (int)instance.amountSlider.value);
                    Refresh();
                });
            }

            BuyRowTemplate.gameObject.SetActive(false);
            SellRowTemplate.gameObject.SetActive(false);
        }

        private void Buy(ItemSO item, int amount)
        {
            if (amount < planet.Items[item])
            {
                planet.Items[item] -= amount;
            }
            else
            {
                planet.Items.Remove(item);
            }
            Inventory.Instance.Credits -= amount * item.BaseCost;

            if (Inventory.Instance.Items.ContainsKey(item))
            {
                Inventory.Instance.Items[item] += amount;
            }
            else
            {
                Inventory.Instance.Items.Add(item, amount);
            }
        }

        private void Sell(ItemSO item, int amount)
        {
            if (amount < Inventory.Instance.Items[item])
            {
                Inventory.Instance.Items[item] -= amount;
            }
            else
            {
                Inventory.Instance.Items.Remove(item);
            }
            Inventory.Instance.Credits += amount * item.BaseCost;

            if (planet.Items.ContainsKey(item))
            {
                planet.Items[item] += amount;
            }
            else
            {
                planet.Items.Add(item, amount);
            }
        }

    }
}
