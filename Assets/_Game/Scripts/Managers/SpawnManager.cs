using System;
using UnityEngine;
using BoardSystem;
using BattleshipSystem;
using System.Collections.Generic;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Instance;
        
        public int boardSize = 10;
        public ShipData[] shipDatas;
        public BoardParts[,] board;
        
        [SerializeField] private BoardParts boardPartsPrefab;
        [SerializeField] private Transform boardParent;
        [SerializeField] private Transform shipParent;

        private List<Vector2Int> usedCoordinates = new List<Vector2Int>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            InitializeBoard();
            SpawnShips();
            GameManager.Instance.IsGameRunning = true;
        }

        private void InitializeBoard()
        {
            board = new BoardParts[boardSize, boardSize];

            for (var i = 0; i < boardSize; i++)
            {
                for (var j = 0; j < boardSize; j++)
                {
                    var boardPartInstance = Instantiate(boardPartsPrefab, new Vector3(i, j, 0), Quaternion.identity, boardParent);
                    board[i, j] = boardPartInstance.GetComponent<BoardParts>();
                }
            }
        }
        
        public BoardParts[,] GetBoard()
        {
            return board;
        }

        private void SpawnShips()
        {
            var totalShips = shipDatas.Length;

            SpawnPlayerShips(totalShips);
            SpawnEnemyShips(totalShips);
        }

        private void SpawnPlayerShips(int count)
        {
            for (var i = 0; i < count; i++)
            {
                SpawnShip(shipDatas[i], true);
            }
        }

        private void SpawnEnemyShips(int count)
        {
            for (var i = 0; i < count; i++)
            {
                SpawnShip(shipDatas[i], false);
            }
        }

        private void SpawnShip(ShipData shipData, bool isPlayerShip)
        {
            var shipSize = shipData.size;
            var isPlaced = false;

            while (!isPlaced)
            {
                var randomX = Random.Range(0, boardSize - shipSize.x + 1);
                var randomY = Random.Range(0, boardSize - shipSize.y + 1);

                var shipRotation = Vector3.zero;

                if (shipSize.x >= 2 || shipSize.y >= 2)
                {
                    var randomOrientation = Random.Range(0, 2);
                    if (randomOrientation == 1)
                    {
                        randomX = Random.Range(0, boardSize - shipSize.y + 1);
                        shipRotation = new Vector3(0, 0, 90);
                    }
                    else
                    {
                        randomY = Random.Range(0, boardSize - shipSize.x + 1);
                    }
                }

                if (!CanPlaceShip(shipSize, randomX, randomY, shipRotation)) continue;
                PlaceShip(shipData, shipSize, randomX, randomY, shipRotation, isPlayerShip);
                isPlaced = true;
            }
        }

        private bool CanPlaceShip(Vector2Int size, int x, int y, Vector3 rotation)
        {
            var maxX = x + (rotation.z == 0 ? size.x : size.y);
            var maxY = y + (rotation.z == 0 ? size.y : size.x);

            if (maxX > boardSize || maxY > boardSize)
            {
                return false;
            }

            for (var i = x; i < maxX; i++)
            {
                for (var j = y; j < maxY; j++)
                {
                    if (board[i, j].hasShip || usedCoordinates.Contains(new Vector2Int(i, j)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void PlaceShip(ShipData shipData, Vector2Int size, int x, int y, Vector3 rotation, bool isPlayerShip)
        {
            var shipPrefab = shipData.shipPrefab;

            var shipPosition = new Vector3(x + (size.x - 1) * 0.5f, y + (size.y - 1) * 0.5f, 0);

            if (size.x >= 2 || size.y >= 2)
            {
                if (rotation.z == 90)
                {
                    shipPosition.x = x + (size.y - 1) * 0.5f;
                    shipPosition.y = y + (size.x - 1) * 0.5f;
                }
                else
                {
                    shipPosition.x = x + (size.x - 1) * 0.5f;
                    shipPosition.y = y + (size.y - 1) * 0.5f;
                }
            }

            var shipInstance = Instantiate(shipPrefab, shipPosition, Quaternion.Euler(rotation));
            var shipController = shipInstance.GetComponent<BattleShipController>();
            BattleshipsManager.Instance.AddToList(isPlayerShip,shipController);
            shipInstance.transform.parent = shipParent;
            
            shipController.Animation.SetVisibility(isPlayerShip);
            shipController.Animation.SetSpriteColor(isPlayerShip);

            for (var i = x; i < x + (rotation.z == 0 ? size.x : size.y); i++)
            {
                for (var j = y; j < y + (rotation.z == 0 ? size.y : size.x); j++)
                {
                    board[i,j].SetBoardPart(true,isPlayerShip,shipInstance,shipController);
                    shipController.Health.AddToList(board[i,j]);
                    usedCoordinates.Add(new Vector2Int(i, j));
                }
            }
        }

    }
}
