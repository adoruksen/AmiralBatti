using System.Collections.Generic;
using System.Collections;
using Ai.TargetSelection;
using BoardSystem;
using Managers;
using UnityEngine;

namespace Players.Ai
{
    public class AiController : MonoBehaviour
    {
        private ITargetSelectionAlgorithm _targetSelectionAlgorithm = null;
        private BoardParts[,] _board;

        IEnumerator Start()
        {
            yield return new WaitUntil(() => GameManager.Instance.IsGameRunning);
            _board = SpawnManager.Instance.GetBoard();
            SetTargetSelectionAlgoritm();
        }

        private void SetTargetSelectionAlgoritm()
        {
            _targetSelectionAlgorithm = GameManager.Instance.difficultyLevel switch
            {
                DifficultyLevel.Easy => new EasyTargetSelection(),
                DifficultyLevel.Medium => new MediumTargetSelection(),
                DifficultyLevel.Hard => new HardTargetSelection(),
                _ => _targetSelectionAlgorithm
            };
            Debug.Log(_targetSelectionAlgorithm);
        }

        IEnumerator PerformAITurnEnumerator()
        {
            yield return new WaitUntil(() => _targetSelectionAlgorithm != null);
            List<Vector2Int> remainingTargets = GetRemainingTargets();

            var selectedTarget = _targetSelectionAlgorithm.SelectTarget(remainingTargets);
            if (!remainingTargets.Contains(selectedTarget)) yield break;
            _board[(int)selectedTarget.x, (int)selectedTarget.y].PerformClick(false);
        }
        public void PerformAITurn()
        {
            StartCoroutine(PerformAITurnEnumerator());
        }
        
        private List<Vector2Int> GetRemainingTargets()
        {
            List<Vector2Int> remainingTargets = new List<Vector2Int>();

            if (_board == null)
            {
                Debug.Log("Board is not initialized.");
                return remainingTargets;
            }

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    if (!_board[i, j].isHit && !_board[i,j].isEnemy)
                    {
                        remainingTargets.Add(new Vector2Int(i, j));
                    }
                }
            }

            return remainingTargets;
        }

    }
}

