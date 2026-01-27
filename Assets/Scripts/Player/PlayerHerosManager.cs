using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHerosManager : MonoBehaviour
{
    public static PlayerHerosManager Instance;
    private const string PLAYER_HERO_KEY = "PlayerHero";
    [Header("Heros database")]
    public HeroesDatabase heroesDatabase;

    private void Awake()
    {
        if (Instance == null) 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
        else 
        { 
            Destroy(gameObject); 
        }
    }
    public bool CheckPlayerHaveHero(string heroID)//Check Player have hero (is Unlocked or is Free)
    {
        if (heroesDatabase == null) Debug.LogError("HeroesDatabase not assigned!");

        if (PlayerPrefs.GetInt(PLAYER_HERO_KEY + heroID, 0) == 1 || heroesDatabase.GetHeroUnlockType(heroID) == UnlockType.DefaultUnlocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GivePlayerHero(string heroID)//Give hero to player (Unlock hero)
    {
        PlayerPrefs.SetInt(PLAYER_HERO_KEY + heroID, 1);
        PlayerPrefs.Save();
    }

    public List<HeroData> GetUnlockedHeroes()//NO FREE HEROES!!!
    {
        List<HeroData> unlockedHeros = new List<HeroData>();
        foreach (HeroData heroData in heroesDatabase.GetAll())
        {
            if (CheckPlayerHaveHero(heroData.id) && heroData.unlockType == UnlockType.ProgressBased)
                unlockedHeros.Add(heroData);
        }
        return unlockedHeros;
    }

    public List<HeroData> GetLockedHeroes()
    {
        List<HeroData> lockedHeros = new List<HeroData>();
        foreach (HeroData heroData in heroesDatabase.GetAll())
        {
            if (!CheckPlayerHaveHero(heroData.id))
                lockedHeros.Add(heroData);
        }
        return lockedHeros;
    }
}
