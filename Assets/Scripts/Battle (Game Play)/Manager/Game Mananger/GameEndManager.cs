using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    [Header("Dependences")]
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

        if (rewardManager != null) rewardManager.IncreasePlayerExprience();
    }

    private void HandleWin()
    {
        Debug.Log("You Win!");

        // Show win UI
        if (gameUIManager != null) gameUIManager.ShowWin();

        // Give rewards
        if (rewardManager != null) rewardManager.IncreaseHeroesExprience(battleManager.GetPlayerHeroes());

    }

    private void HandleLose()
    {
        Debug.Log("Game Over!");

        // Show lose UI
        if (gameUIManager != null) gameUIManager.ShowLose();

    }
}
