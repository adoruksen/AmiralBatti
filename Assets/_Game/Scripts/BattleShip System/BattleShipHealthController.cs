using System.Collections.Generic;
using BoardSystem;
using Managers;
using UISystem;
using UnityEngine;

namespace BattleshipSystem
{
    public class BattleShipHealthController : MonoBehaviour
    {
        [SerializeField] private string battleshipName;
        public List<BoardParts> BoardPartsList;

        public bool isSink = false;
        public bool isDamaged = false;

        public void AddToList(BoardParts parts)
        {
            if (BoardPartsList.Contains(parts)) return;
            BoardPartsList.Add(parts);
        }

        private bool CheckShipSink()
        {
            foreach (var item in BoardPartsList)
            {
                if (!item.isHit) return false;
            }

            return true;
        }

        public void DamageShip()
        {
            if (CheckShipSink())
            {
                isSink = true;
                UIController.Instance.SetBattleShipInfo(battleshipName,true);
                BattleshipsManager.Instance.OnShipSinkEventInvoke();
                foreach (var item in BoardPartsList)
                {
                    item.Animation.SinkHandle();
                }
            }
            else
            {
                isDamaged = true;
                UIController.Instance.SetBattleShipInfo(battleshipName,false);
            }
        }
    }
}

