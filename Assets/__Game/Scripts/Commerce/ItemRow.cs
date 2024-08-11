using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game
{

    public class ItemRow : MonoBehaviour
    {

        public TextMeshProUGUI itemName;
        public TextMeshProUGUI quantityText;
        public TextMeshProUGUI priceText;
        public Slider amountSlider;
        public TextMeshProUGUI amountText;
        public Button button;

        private void Start()
        {
            amountSlider.onValueChanged.AddListener((x) => amountText.text = x.ToString());            
        }

        public void Assign(ItemSO item, int quantity)
        {
            itemName.text = item.Name;
            quantityText.text = quantity.ToString();
            priceText.text = item.BaseCost.ToString();
            amountSlider.value = 0;
            amountSlider.maxValue = quantity;
            amountText.text = "0";
        }
    }

}
