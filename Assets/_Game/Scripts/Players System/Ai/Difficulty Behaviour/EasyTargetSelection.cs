using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ai.TargetSelection
{
    public class EasyTargetSelection : ITargetSelectionAlgorithm 
    {
        private List<Vector2Int> _firedTargets = new List<Vector2Int>();

        public Vector2Int SelectTarget(List<Vector2Int> remainingTargets) {
            var availableTargets = remainingTargets.Except(_firedTargets).ToList();

            if (availableTargets.Count <= 0) return Vector2Int.zero;
            var selectedTarget = GetRandomTarget(availableTargets);
            _firedTargets.Add(selectedTarget);
            return selectedTarget;

        }

        private Vector2Int GetRandomTarget(List<Vector2Int> targets) 
        {
            var randomIndex = Random.Range(0, targets.Count);
            return targets[randomIndex];
        }
    }
}

