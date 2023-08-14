using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleshipSystem
{
    public class BattleShipController : MonoBehaviour
    {
        public BattleShipAnimationController Animation { get; private set; }
        public BattleShipHealthController Health { get; private set; }

        private void Awake()
        {
            Animation = GetComponent<BattleShipAnimationController>();
            Health = GetComponent<BattleShipHealthController>();
        }
    }
}

