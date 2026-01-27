using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private RewardManager rewardManager;
    private void OnEnable()
    {
        BattleManager.OnBattleEnded += HandleGameEnd;
    }

    private void OnDisable()
    {
        BattleManager.OnBattleEnded -= HandleGameEnd;
    }

    private void HandleGameEnd(GameState state)
    {
        switch (state)
        {
            case GameState.Win:
                HandleWin();
                break;

            case GameState.GameOver:
                HandleLose();
                break;
        }

        rewardManager.IncreasePlayerExprience();
    }

    private void HandleWin()
    {
        Debug.Log("You Win!");
        // Show win UI
        gameUIManager.ShowWin();
        // Give rewards
        rewardManager.IncreaseHeroesExprience(battleManager.GetPlayerHeroes());
        // Save progress
    }

    private void HandleLose()
    {
        Debug.Log("Game Over!");
        gameUIManager.ShowLose();

        // Show lose UI
        // Retry / Back to menu
    }
}
