namespace GameSystem
{
    public class EnemyGameState : BaseGameState
    {
        protected override void OnStateEnter(GameController controller)
        {
            controller.AiController.PerformAITurn();
        }
    }


}

