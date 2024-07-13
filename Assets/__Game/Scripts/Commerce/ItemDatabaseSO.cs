using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "Scriptable Objects/ItemDatabaseSO")]
    public class ItemDatabaseSO : ScriptableObject
    {

        public List<ItemSO> Items;

        public ItemSO GetRandomItem()
        {
            return Items[Random.Range(0, Items.Count)];
        }
    }
}
