using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Game
{

    public class PlanetTrigger : TriggerEvent
    {

        [field: SerializeField] private TextMeshPro PlanetNameTmp { get; set; }

        private void Start()
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
            PlanetNameTmp.text = $"Planet {GetRandomGreekLetter()}-{Random.Range(1, 99)}";
        }

        static string[] letters = new string[] { "Alpha", "Beta", "Gamma", "Delta", "Chi", "Epsilon" };

        private string GetRandomGreekLetter()
        {
            return letters[Random.Range(0, letters.Length)];
        }
    }

}
