using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private HeroesDatabase heroesDatabase;

    [SerializeField]private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    private List<Hero> playerHeroes = new List<Hero>();
    private Hero enemyHero;

   // public static Hero activePlayerHero;
    public static Hero activeEnemyHero;
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
        playerHeroes = SpawnPlayerHeroes();
        enemyHero = activeEnemyHero = SpawnEnemyHero(enemyHeroData);

        //Start Game
        //Enemy
        enemyHero.GetComponent<EnemyHero>().StartAttack(playerHeroes);




    }
    public static bool isBattleBusy = false;
    public static bool isPlayerTurn = true;
    void PlayBattleRound()
    {

        if (isPlayerTurn)
        {
            isBattleBusy = true;
        }
        else
        {


            isPlayerTurn = true;
        }
        //isPlayerTurn = !isPlayerTurn;
    }
    private List<Hero> SpawnPlayerHeroes()
    {
        List<HeroData> playerDeckCards = new List<HeroData>();
        List<Hero> spawnedHeroes = new List<Hero>();

        playerDeckCards = playerDeckManager.GetPlayerDeckCards();
        spawnedHeroes = spawner.SpawnPlayerHeroes(playerDeckCards);

        return spawnedHeroes;
    }

    private Hero SpawnEnemyHero(HeroData heroData)
    {
        return spawner.SpawnEnemyHero(heroData);
    }
}
