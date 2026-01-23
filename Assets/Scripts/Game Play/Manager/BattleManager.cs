using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private HeroesDatabase heroesDatabase;

    [SerializeField]private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    private List<Hero> playerHeroes = new List<Hero>();
    private Hero enemyHero;
    private void Start()
    {
        SetupGame();
    }


    private void SetupGame()
    {
        //Enemy Hero Data (static for test)
        HeroData enemyHeroData = new HeroData();
        enemyHeroData = heroesDatabase.GetHeroDataByIndex(2);


        //Spawn
        SpawnPlayerHeroes();
        SpawnEnemyHero(enemyHeroData);


        
    }

    private void SpawnPlayerHeroes()
    {
        List<HeroData> playerDeckCards = new List<HeroData>();
        playerDeckCards = playerDeckManager.GetPlayerDeckCards();
        playerHeroes = spawner.SpawnPlayerHeroes(playerDeckCards);
    }

    private void SpawnEnemyHero(HeroData heroData)
    {
        enemyHero = spawner.SpawnEnemyHero(heroData);
    }
}
