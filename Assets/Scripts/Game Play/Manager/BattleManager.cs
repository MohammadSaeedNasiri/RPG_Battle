using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;
    [SerializeField] private HeroesDatabase heroesDatabase;

    [SerializeField]private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    private List<Hero> playerHeroes = new List<Hero>();
    public Hero enemyHero;

   // public static Hero activePlayerHero;
    //public Hero activeEnemyHero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 
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
        enemyHero = SpawnEnemyHero(enemyHeroData);

        //Start Game
        //Enemy




    }
    public bool isBattleBusy = false;
    public bool isPlayerTurn = true;

    public void OnAttackComplete()
    {
        //Check State
        PrepareNextBattleRound();
    }
    private void PrepareNextBattleRound()
    {
        if (isPlayerTurn)
        {
            isBattleBusy = true;
        }
        else
        {
            enemyHero.GetComponent<EnemyHero>().StartAttack(playerHeroes);
            isPlayerTurn = true;
        }
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
