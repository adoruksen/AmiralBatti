using UnityEngine;
using System.Collections.Generic;

namespace Ai.TargetSelection
{
    public interface ITargetSelectionAlgorithm 
    {
        Vector2Int SelectTarget(List<Vector2Int> remainingTargets);
    }
}

