using System;
using System.Collections;
using System.Collections.Generic;
using BattleshipSystem;
using GameSystem;
using UnityEngine;

namespace Managers
{
    public class BattleshipsManager : MonoBehaviour
    {
        public static BattleshipsManager Instance;

        public List<BattleShipController> EnemyBattleShips;
        public List<BattleShipController> PlayerBattleShips;

        public event Action OnShipSink;
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            OnShipSink += OnShipSinkHandle;
        }

        private void OnDisable()
        {
            OnShipSink -= OnShipSinkHandle;
        }

        public void AddToList(bool isPlayer,BattleShipController battleship)
        {
            switch (isPlayer)
            {
                case true when !PlayerBattleShips.Contains(battleship):
                    PlayerBattleShips.Add(battleship);
                    break;
                case false when !EnemyBattleShips.Contains(battleship):
                    EnemyBattleShips.Add(battleship);
                    break;
            }
        }

        public void OnShipSinkEventInvoke() => OnShipSink?.Invoke();

        private bool CheckAllSink(List<BattleShipController> battleships)
        {
            foreach (var item in battleships)
            {
                if (!item.Health.isSink) return false;
            }
            return true;
        }

        private void OnShipSinkHandle()
        {
            if (GameController.Instance.turn == Turn.Ai)
            {
                if(CheckAllSink(PlayerBattleShips))
                    GameManager.Instance.CompleteGame(false);
            }
            else
            {
                if(CheckAllSink(EnemyBattleShips))
                    GameManager.Instance.CompleteGame(true);
            }
        }
    }
}

