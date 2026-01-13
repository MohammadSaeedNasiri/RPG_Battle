using UnityEngine;

public class PlayerHerosManager : MonoBehaviour
{
    public static PlayerHerosManager Instance;
    private const string PLAYER_HERO_KEY = "PlayerHero";
    public HerosData herosData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public bool CheckPlayerHaveHero(string heroID)
    {
        if (PlayerPrefs.GetInt(PLAYER_HERO_KEY + heroID, 0) == 1 || herosData.CheckIsHeroFree(heroID))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GivePlayerHero(string heroID)
    {
        PlayerPrefs.SetInt(PLAYER_HERO_KEY + heroID, 1);
        PlayerPrefs.Save();
    }
}
