using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem
{
    public class UIController : MonoBehaviour
    {
        public static UIController Instance;
        
        [SerializeField] private GameObject finishUI;
        [SerializeField] private GameObject GameBoardUI;
        [SerializeField] private TMP_Text battleshipInfoText;
        
        
        [Header("Enemy")] 
        [SerializeField] private TMP_Text enemyAccurateText;
        [SerializeField] private TMP_Text enemyMissedText;
        private int _enemyAccurate = 0;
        private int _enemyMissed = 0;
        
        [Header("Player")] 
        [SerializeField] private TMP_Text playerAccurateText;
        [SerializeField] private TMP_Text playerMissedText;
        private int _playerAccurate = 0;
        private int _playerMissed = 0;

        [Header("Finish UI")]
        [SerializeField] private TMP_Text winnerText;
        [SerializeField] private TMP_Text accurateText;
        [SerializeField] private TMP_Text missedText;
        [SerializeField] private TMP_Text gameTimeText;
        [SerializeField] private Button playAgainBtn;
        private float _gameTime = 0f;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void OnEnable()
        {
            playAgainBtn.onClick.AddListener(PlayAgainButtonHandle);
        }

        private void OnDisable()
        {
            playAgainBtn.onClick.RemoveListener(PlayAgainButtonHandle);
        }

        private void Update()
        {
            if (!GameManager.Instance.IsGameRunning) return;
            _gameTime += Time.deltaTime;
        }

        public void SetAccurateText(bool isPlayer)
        {
            if (isPlayer)
            {
                _playerAccurate++;
                playerAccurateText.text = $"Accurate: {_playerAccurate}";
            }
            else
            {
                _enemyAccurate++;
                enemyAccurateText.text = $"Accurate: {_enemyAccurate}";
            }
        }

        public void SetMissedText(bool isPlayer)
        {
            if (isPlayer)
            {
                _playerMissed++;
                playerMissedText.text = $"Missed: {_playerMissed}";
            }
            else
            {
                _enemyMissed++;
                enemyMissedText.text = $"Missed: {_enemyMissed}";
            }
        }

        public void SetFinishUI(bool isPlayer)
        {
            GameBoardUI.SetActive(false);
            finishUI.SetActive(true);
            if (isPlayer)
            {
                winnerText.text = $"WINNER: YOU";
                accurateText.text = $"Accurate: {_playerAccurate}";
                missedText.text = $"Missed: {_playerMissed}";
            }
            else
            {
                winnerText.text = $"WINNER: ENEMY";
                accurateText.text = $"Accurate: {_enemyAccurate}";
                missedText.text = $"Missed: {_enemyMissed}";
            }

            var gameTimeF1 = _gameTime.ToString("F1");
            gameTimeText.text = $"GameTime: {gameTimeF1}";
        }

        public void SetBattleShipInfo(string battleshipName, bool isSink)
        {
            battleshipInfoText.text = isSink ? $"{battleshipName} is sink!" : $"{battleshipName} is damaged!";
            BattleShipInfoOpen();
            Invoke(nameof(BattleshipInfoClose),1f);
        }

        private void BattleShipInfoOpen()
        {
            var infoObject = battleshipInfoText.gameObject;
            infoObject.SetActive(true);
        }
        private void BattleshipInfoClose()
        {
            var infoObject = battleshipInfoText.gameObject;
            infoObject.SetActive(false);
        }


        private void PlayAgainButtonHandle()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
