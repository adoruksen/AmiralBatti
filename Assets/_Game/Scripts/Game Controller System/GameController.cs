using System.Collections;
using Managers;
using Players.Ai;
using UnityEngine;

namespace GameSystem
{
    public enum Turn
    {
        Player,
        Ai
    }
    public class GameController : MonoBehaviour
    {
        public static GameController Instance;

        [SerializeField] public AiController AiController;
        
        public Turn turn = Turn.Player;
        
        [HideInInspector] public EnemyGameState EnemyState = new EnemyGameState();
        [HideInInspector] public PlayerGameState PlayerState = new PlayerGameState();
        
        public BaseGameState CurrentState { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

        IEnumerator Start()
        {
            yield return new WaitUntil(() => GameManager.Instance.IsGameRunning);
            if(turn == Turn.Ai)
                SetState(EnemyState);
            else SetState(PlayerState);
        }

        private void Update()
        {
            CurrentState?.OnStateUpdate(this);
        }

        private void FixedUpdate()
        {
            CurrentState?.OnStateFixedUpdate(this);
        }

        private void ExitState()
        {
            CurrentState?.StateExit(this);
        }

        private void SetState(BaseGameState newState)
        {
            ExitState();
            CurrentState = newState;
            CurrentState.StateEnter(this);
        }

        public void ChangeTurn()
        {
            if (turn == Turn.Ai)
            {
                turn = Turn.Player;
                SetState(PlayerState);
            }
            else
            {
                turn = Turn.Ai;
                SetState(EnemyState);
            }
        }

        public void DontChangeTurn()
        {
            var currentState = CurrentState;
            ExitState();
            currentState.StateEnter(this);
        }
        
    }
}

