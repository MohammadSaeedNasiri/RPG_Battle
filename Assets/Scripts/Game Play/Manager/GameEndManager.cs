using UnityEngine;

public class GameEndManager : MonoBehaviour
{
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
    }

    private void HandleWin()
    {
        Debug.Log("You Win!");
        // Show win UI
        // Give rewards
        // Save progress
    }

    private void HandleLose()
    {
        Debug.Log("Game Over!");
        // Show lose UI
        // Retry / Back to menu
    }
}
