using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ai.TargetSelection
{
    public class MediumTargetSelection : ITargetSelectionAlgorithm {
        private List<Vector2Int> _firedTargets = new List<Vector2Int>();
        private List<Vector2Int> _directions = new List<Vector2Int> {
            new Vector2Int(0, 1),  // Yukarı
            new Vector2Int(0, -1), // Aşağı
            new Vector2Int(-1, 0), // Sol
            new Vector2Int(1, 0)   // Sağ
        };

        public Vector2Int SelectTarget(List<Vector2Int> remainingTargets) {
            var availableTargets = remainingTargets.Except(_firedTargets).ToList();

            if (availableTargets.Count <= 0) return GetRandomTarget(remainingTargets);
            var selectedTarget = GetSmartTarget(availableTargets);
            _firedTargets.Add(selectedTarget);
            return selectedTarget;

        }

        private Vector2Int GetSmartTarget(List<Vector2Int> targets)
        {
            foreach (var newTarget in _firedTargets.SelectMany(prevTarget =>
                         _directions, (prevTarget, direction) => prevTarget + direction).Where(newTarget => 
                         IsValidTarget(newTarget) && targets.Contains(newTarget)))
            {
                return newTarget;
            }

            return GetRandomTarget(targets);
        }

        private Vector2Int GetRandomTarget(List<Vector2Int> targets) 
        {
            var randomIndex = Random.Range(0, targets.Count);
            return targets[randomIndex];
        }

        private bool IsValidTarget(Vector2Int target) 
        {
            return target.x is >= 0 and < 10 && target.y is >= 0 and < 10;
        }
    }

}

