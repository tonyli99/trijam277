using UnityEngine;

namespace Game
{

    [CreateAssetMenu(fileName = "ItemSO", menuName = "Scriptable Objects/ItemSO")]
    public class ItemSO : ScriptableObject
    {
        public string Name;
        public int BaseCost;
    }
}
