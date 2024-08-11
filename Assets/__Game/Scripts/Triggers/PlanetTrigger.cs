using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{

    public class PlanetTrigger : MonoBehaviour
    {

        [field: SerializeField] private TextMeshPro PlanetNameTmp { get; set; }
        [field: SerializeField] private GameObject LandPrompt { get; set; }
        [field: SerializeField] private ItemDatabaseSO ItemDatabase {  get; set; }

        public string PlanetName { get; private set; }

        public Dictionary<ItemSO, int> Items { get; private set; } = new Dictionary<ItemSO, int>();

        private void Start()
        {
            PlanetName = $"Planet {GetRandomGreekLetter()}-{Random.Range(1, 99)}";
            PlanetNameTmp.text = PlanetName;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
            for (int i = 0; i < Random.Range(2, 5); i++)
            {
                var item = ItemDatabase.Items[i];
                var amount = Random.Range(2, 20);
                if (Items.ContainsKey(item))
                {
                    Items[item] += amount;
                }
                else
                {
                    Items.Add(item, amount);
                }
            }
        }

        static string[] letters = new string[] { "Alpha", "Beta", "Gamma", "Delta", "Chi", "Epsilon" };

        private string GetRandomGreekLetter()
        {
            return letters[Random.Range(0, letters.Length)];
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                LandPrompt.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(Tags.Player))
            {
                LandPrompt.SetActive(false);
            }
        }

        private void Update()
        {
            if (LandPrompt.activeSelf)
            {
                if (Input.GetKeyDown(InputAction.LandKey))
                {
                    Land();
                }
            }
        }

        private void Land()
        {
            Time.timeScale = 0;
            FindFirstObjectByType<PlayerShipController>().InstantStop();
            CommerceUI.Instance.Open(this);
        }

        public void Leave()
        {
            CommerceUI.Instance.Close();
            Time.timeScale = 1;
        }

        public void InitializeStock(ItemDatabaseSO itemDatabase)
        { }

    }

}
