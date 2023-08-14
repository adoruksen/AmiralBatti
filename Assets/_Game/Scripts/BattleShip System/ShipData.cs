using UnityEngine;

namespace BattleshipSystem
{
    [CreateAssetMenu(fileName = "New Ship Data", menuName = "Ship System/Ship Data", order = 1)]
    public class ShipData : ScriptableObject 
    {
        public Vector2Int size;
        public GameObject shipPrefab;
    }
}
