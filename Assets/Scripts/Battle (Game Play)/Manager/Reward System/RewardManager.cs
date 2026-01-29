using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    #region Constants
    private const int PLAYER_EXP_PER_BATTLE = 1;// Player EXP gained per battle 
    public static int HERO_EXP_PER_WIN = 5;// EXP given to each hero per win
    private const int EXP_PER_HERO = 3;// Player EXP required to unlock a new hero
    private const int MAX_ACTIVE_HERO = 10 - 3;//-3 FOR Free heros (3 free hero + 7 unlocked hero = 10 heroes)

    #endregion 

    [Header("Dependences")]
    [SerializeField] private HeroesDatabase heroesDatabase;
    [SerializeField] private PlayerHerosManager playerHerosManager;
    [SerializeField] private PlayerExperienceManager playerExperienceManager;
    [SerializeField] private HeroExprienceManager heroExperienceManager;
    [SerializeField] private GameUIManager gameUIManager;


    public void IncreasePlayerExprience()
    {
        playerExperienceManager.AddPlayerExprience(PLAYER_EXP_PER_BATTLE);
        TryUnlockHeroes(playerExperienceManager.GetPlayerExprience());
    }
    // Increase player EXP and check for new hero unlock
    public void IncreaseHeroesExprience(List<Hero> playerHeroes)
    {

        for (int i = 0; i < playerHeroes.Count; i++)
        {
            HeroRuntimeData heroRuntimeData = playerHeroes[i].GetHeroRuntimeData();



            if (playerHeroes[i].GetIsAlive() && heroRuntimeData.heroExprienceData.exprience < heroRuntimeData.heroData.maxExprience)
            {
                // Show EXP gain in UI and apply EXP
                gameUIManager.ShowHeroUpgradeExpReward(i, heroRuntimeData, HERO_EXP_PER_WIN);
                heroExperienceManager.AddHeroExprience(heroRuntimeData.heroData.GetID(), HERO_EXP_PER_WIN);
            }
            else
            {
                // Show no EXP gain (dead or max EXP)
                gameUIManager.ShowHeroUpgradeExpReward(i, heroRuntimeData, 0);

            }
        }

    }

    // Try to unlock a new hero based on player EXP
    public void TryUnlockHeroes(int playerExp)
    {
        int shouldUnlockNewHero = playerExp / EXP_PER_HERO;
        List<HeroData> unlockedHeroes = playerHerosManager.GetUnlockedHeroes();



        if (shouldUnlockNewHero - unlockedHeroes.Count == 0)
            return;
        if (MAX_ACTIVE_HERO - unlockedHeroes.Count == 0)
            return;


        List<HeroData> lockedHeroes = playerHerosManager.GetLockedHeroes();

        HeroData selectedHeroForUnlock = GetRandomHero(lockedHeroes);
        if(selectedHeroForUnlock != null)
        {
            gameUIManager.ShowNewHeroUnlocked(selectedHeroForUnlock);
            playerHerosManager.GivePlayerHero(selectedHeroForUnlock.id);

            Debug.Log("Hero " + selectedHeroForUnlock.id + " is unlocked.");
        }
    }

    // Get a random hero from the locked heroes list
    private HeroData GetRandomHero(List<HeroData> lockedHeroes)
    {
        if (lockedHeroes == null || lockedHeroes.Count == 0)
            return null;

        int randomIndex = Random.Range(0, lockedHeroes.Count);
        return lockedHeroes[randomIndex];
    }
}
