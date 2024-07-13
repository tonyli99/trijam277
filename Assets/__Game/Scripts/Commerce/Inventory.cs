using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Inventory : MonoBehaviour
    {        

        public static Inventory Instance { get; private set; }

        public const int StartingCredits = 50;

        public Dictionary<ItemSO, int> Items { get; private set; } = new Dictionary<ItemSO, int>();
        public int Credits { get; set; } = StartingCredits;

        private void Awake()
        {
            Instance = this;
        }

        public void Add(ItemSO item, int quantity)
        {
            if (!Items.ContainsKey(item)) Items[item] = 0;
            Items[item] += quantity;
        }

    }
}
