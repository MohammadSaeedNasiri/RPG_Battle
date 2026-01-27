using UnityEngine;

public class PlayerExperienceManager : MonoBehaviour
{
    private const string PLAYER_EXPRIENCE_KEY = "PlayerExprience";

    //[SerializeField]
    //private HeroesDatabase heroesDatabase;



    //Player Exprience
    public void AddPlayerExprience(int valueForAdd)
    {
        int exprience = PlayerPrefs.GetInt(PLAYER_EXPRIENCE_KEY, 0);
        exprience += valueForAdd;
        PlayerPrefs.SetInt(PLAYER_EXPRIENCE_KEY, exprience);
        PlayerPrefs.Save();
    }
    public int GetPlayerExprience(string heroID)
    {
        int exprience = PlayerPrefs.GetInt(PLAYER_EXPRIENCE_KEY, 0);
        return exprience;
    }


}
