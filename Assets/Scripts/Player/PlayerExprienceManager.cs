using UnityEngine;

public class PlayerExprienceManager : MonoBehaviour
{
    public static PlayerExprienceManager Instance;

    private const string PLAYER_PLAYED_WAR_COUNT_KEY = "PlayerPlayedWarCount";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public void IncreasePlayerPlayedWarCount()
    {
        int userPlayedWarCount = PlayerPrefs.GetInt(PLAYER_PLAYED_WAR_COUNT_KEY, 0);
        userPlayedWarCount++;
        PlayerPrefs.SetInt(PLAYER_PLAYED_WAR_COUNT_KEY, userPlayedWarCount);
        PlayerPrefs.Save();
    }

    public int GetPlayerPlayedWarCount()
    {
        return PlayerPrefs.GetInt(PLAYER_PLAYED_WAR_COUNT_KEY, 0);
    }
}
