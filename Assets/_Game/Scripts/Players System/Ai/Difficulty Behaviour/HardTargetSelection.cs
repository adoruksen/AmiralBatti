using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ai.TargetSelection
{
    public class HardTargetSelection : ITargetSelectionAlgorithm
    {
        private List<Vector2Int> _firedTargets = new List<Vector2Int>();
        private List<Vector2Int> _shipPositions = new List<Vector2Int>();
        private List<Vector2Int> _highPriorityTargets = new List<Vector2Int>();

        private List<Vector2Int> _directions = new List<Vector2Int>
        {
            new Vector2Int(0, 1), // Yukarı
            new Vector2Int(0, -1), // Aşağı
            new Vector2Int(-1, 0), // Sol
            new Vector2Int(1, 0) // Sağ
        };

        public Vector2Int SelectTarget(List<Vector2Int> remainingTargets)
        {
            List<Vector2Int> availableTargets = remainingTargets.Except(_firedTargets).ToList();

            if (availableTargets.Count > 0)
            {
                Vector2Int selectedTarget = GetSmartTarget(availableTargets);
                _firedTargets.Add(selectedTarget);
                return selectedTarget;
            }

            return Vector2Int.zero; // Tüm hedefler ateşlendi, geçersiz hedef döndür
        }

        private Vector2Int GetSmartTarget(List<Vector2Int> targets)
        {
            Dictionary<Vector2Int, int> targetScores = new Dictionary<Vector2Int, int>();

            foreach (Vector2Int target in targets)
            {
                int score = CalculateTargetScore(target);
                targetScores.Add(target, score);
            }

            Vector2Int bestTarget = targetScores.OrderByDescending(kv => kv.Value).First().Key;
            return bestTarget;
        }

        private int CalculateTargetScore(Vector2Int target)
        {
            int distanceScore = CalculateDistanceScore(target);
            int hitAreaScore = CalculateHitAreaScore(target);
            int highPriorityScore = CalculateHighPriorityScore(target);

            int totalScore = distanceScore + hitAreaScore + highPriorityScore;

            return totalScore;
        }

        private int CalculateDistanceScore(Vector2Int target)
        {
            int totalDistance = 0;

            foreach (Vector2Int shipPosition in _shipPositions)
            {
                int distance = Mathf.Abs(target.x - shipPosition.x) + Mathf.Abs(target.y - shipPosition.y);
                totalDistance += distance;
            }

            return totalDistance;
        }

        private int CalculateHitAreaScore(Vector2Int target)
        {
            var hitAreaCount = 0;

            foreach (Vector2Int direction in _directions)
            {
                Vector2Int adjacentCell = target + direction;

                if (_firedTargets.Contains(adjacentCell))
                {
                    hitAreaCount++;
                }
            }

            return hitAreaCount;
        }

        private int CalculateHighPriorityScore(Vector2Int target)
        {
            return _highPriorityTargets.Contains(target) ? 100 : 0; // Yüksek öncelikli hedefleri ödüllendir
        }

        public void SetHighPriorityTargets(List<Vector2Int> highPriorityTargets)
        {
            _highPriorityTargets = highPriorityTargets;
        }
    }
}
