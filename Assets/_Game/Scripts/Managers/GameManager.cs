using System;
using System.Collections;
using System.Collections.Generic;
using UISystem;
using UnityEngine;

namespace Managers
{
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard
    }
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public DifficultyLevel difficultyLevel = DifficultyLevel.Easy;

        public bool IsGameRunning { get; set; } = false;
        private void Awake()
        {
            Instance = this;
        }
        
        public void CompleteGame(bool isWin)
        {
            IsGameRunning = false;
            UIController.Instance.SetFinishUI(isWin);
        }
    }
}
