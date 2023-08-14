using System;
using UnityEngine;
using BattleshipSystem;
using GameSystem;
using UISystem;

namespace BoardSystem
{
    public class BoardParts : MonoBehaviour
    {
        public BoardPartAnimationController Animation { get; private set; }
        
        public bool hasShip;
        public bool isHit;
        public bool isEnemy;
        public bool isPlayer;
        public GameObject shipInstance;
        public BattleShipController shipController;

        private void Awake()
        {
            Animation = GetComponent<BoardPartAnimationController>();
        }

        public void SetBoardPart(bool hasBattleship,bool player,GameObject shipObject,BattleShipController controller)
        {
            hasShip = hasBattleship;
            switch (player)
            {
                case true:
                    isPlayer = true;
                    break;
                case false:
                    isEnemy = true;
                    break;
            }

            shipInstance = shipObject;
            shipController = controller;
        }

        private void OnMouseDown()
        {
            if (GameController.Instance.turn != Turn.Player) return;
            PerformClick(true);
        }

        public void PerformClick(bool isPlayerClick)
        {
            switch (isPlayerClick)
            {
                case true when isPlayer:
                case false when isEnemy:
                    return;
            }
            if (isHit) return;
            
            isHit = true;
            if (hasShip)
            {
                UIController.Instance.SetAccurateText(isPlayerClick);
                shipController.Health.DamageShip();
                GameController.Instance.DontChangeTurn();
                Animation.HitHandle();
            }
            else
            {
                UIController.Instance.SetMissedText(isPlayerClick);
                GameController.Instance.ChangeTurn();
                Animation.MissHandle();
            }
        }
    }
}

