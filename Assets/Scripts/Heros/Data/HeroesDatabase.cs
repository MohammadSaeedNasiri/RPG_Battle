using System.Collections.Generic;
using UnityEngine;

//For enter and save player and enemy heroes Data
[CreateAssetMenu(fileName = "HeroesData", menuName = "Heroes/HeroesDatabase")]
public class HeroesDatabase : ScriptableObject
{
    [SerializeField] private List<HeroData> playerHeroesData;//player heroes
    [SerializeField] private List<HeroData> enemyHeroesData;//enemy heroes

    public int PlayerHeroesCount => playerHeroesData.Count;

    //Select player herodata with hero index
    public HeroData GetPlayerHeroDataByIndex(int index)
    {
        if (index < 0 || index >= playerHeroesData.Count)
        {
            Debug.LogError("Hero index out of range");
            return null;
        }

        return playerHeroesData[index];
    }

    //Select herodata with hero ID
    public HeroData GetHeroDataByID(string heroId)
    {

        foreach (var hero in playerHeroesData)
        {
            if (hero.id == heroId)
                return hero;
        }


        foreach (var hero in enemyHeroesData)
        {
            if (hero.id == heroId)
                return hero;
        }


        Debug.LogError($"Hero with ID {heroId} not found");
        return null;
    }

    //Get hero unlock type
    public UnlockType GetPlayerHeroUnlockType(string heroID)//Check is hero free to use
    {
        return GetHeroDataByID(heroID).unlockType;
    }

    //Get All Player heroes
    public List<HeroData> GetAllPlayerHeroes()
    {
        return playerHeroesData;
    }

    //Get Random Hero Data
    public HeroData GetEnemyHeroDataRandomly()
    {
        int index = Random.Range(0, enemyHeroesData.Count);
        HeroData hero = enemyHeroesData[index];
        return enemyHeroesData[index];
    }
}
