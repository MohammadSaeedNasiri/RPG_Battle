using System.Collections.Generic;
using UnityEngine;

//For enter and save heroes 
[CreateAssetMenu(fileName = "HeroesData", menuName = "Heroes/HeroesDatabase")]
public class HeroesDatabase : ScriptableObject
{
    [SerializeField]
    private List<HeroData> heroesData;

    public int Count => heroesData.Count;

    public HeroData GetHeroDataByIndex(int index)
    {
        if (index < 0 || index >= heroesData.Count)
        {
            Debug.LogError("Hero index out of range");
            return null;
        }

        return heroesData[index];
    }

    public HeroData GetHeroDataByID(string heroId)
    {
        foreach (var hero in heroesData)
        {
            if (hero.id == heroId)
                return hero;
        }

        Debug.LogError($"Hero with ID {heroId} not found");
        return null;
    }
    public UnlockType GetHeroUnlockType(string heroID)//Check is hero free to use
    {
        return GetHeroDataByID(heroID).unlockType;
    }
    public List<HeroData> GetAll()
    {
        return heroesData;
    }
}
