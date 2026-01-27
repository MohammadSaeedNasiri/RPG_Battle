using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    [SerializeField] private const int PLAYER_EXP_PER_BATTLE = 3;
    public static int HERO_EXP_PER_WIN = 5;


    [SerializeField] private PlayerExperienceManager playerExperienceManager;
    [SerializeField] private HeroExprienceManager heroExperienceManager;
    [SerializeField] private GameUIManager gameUIManager;
    public void IncreasePlayerExprience()
    {
        playerExperienceManager.AddPlayerExprience(PLAYER_EXP_PER_BATTLE);
    }

    public void IncreaseHeroesExprience(List<Hero> playerHeroes)
    {

        for (int i = 0; i < playerHeroes.Count; i++)
        {
            HeroRuntimeData heroRuntimeData = playerHeroes[i].GetHeroRuntimeData();



            if (playerHeroes[i].GetIsAlive() && heroRuntimeData.heroExprienceData.exprience < heroRuntimeData.heroData.maxExprience)
            {
                gameUIManager.ShowHeroUpgradeExpReward(i, heroRuntimeData, HERO_EXP_PER_WIN);
                heroExperienceManager.AddHeroExprience(heroRuntimeData.heroData.GetID(), HERO_EXP_PER_WIN);
            }
            else
            {
                gameUIManager.ShowHeroUpgradeExpReward(i, heroRuntimeData, 0);

            }
        }

    }
}
