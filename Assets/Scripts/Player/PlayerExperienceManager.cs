using UnityEngine;


//This code using for player battle count 
//Important Note : Player Ended Battles Count = Player Exprience
public class PlayerExperienceManager : MonoBehaviour
{
    private const string PLAYER_EXPRIENCE_KEY = "PlayerExprience";


    //Player Exprience

    //Add Player Exprience
    public void AddPlayerExprience(int valueForAdd)
    {
        int exprience = PlayerPrefs.GetInt(PLAYER_EXPRIENCE_KEY, 0);
        exprience += valueForAdd;
        PlayerPrefs.SetInt(PLAYER_EXPRIENCE_KEY, exprience);
        PlayerPrefs.Save();

        Debug.Log("New player exp :" + exprience);
    }

    //Get Player Exprience
    public int GetPlayerExprience()
    {
        int exprience = PlayerPrefs.GetInt(PLAYER_EXPRIENCE_KEY, 0);
        return exprience;
    }


}
