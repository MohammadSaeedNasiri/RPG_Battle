using UnityEngine;

public class PlayerExprienceManager : MonoBehaviour
{
    public static PlayerExprienceManager Instance;

    private const string PLAYER_PALYED_WAR_COUNT = "PlayerPlayedWarCount";

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
        int userPlayedWarCount = PlayerPrefs.GetInt(PLAYER_PALYED_WAR_COUNT, 0);
        userPlayedWarCount++;
        PlayerPrefs.SetInt(PLAYER_PALYED_WAR_COUNT, userPlayedWarCount);
        PlayerPrefs.Save();
    }

    public int GetPlayerPlayedWarCount()
    {
        return PlayerPrefs.GetInt(PLAYER_PALYED_WAR_COUNT, 0);
    }
}
