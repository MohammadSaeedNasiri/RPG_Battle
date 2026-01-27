using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[Serializable]
public enum GameState
{
    Playing,
    Paused,
    GameOver,
    Win
}


public class BattleManager : MonoBehaviour
{

    public static BattleManager Instance;
    [SerializeField] private HeroesDatabase heroesDatabase;

    [SerializeField] private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    [SerializeField] private TextMeshProUGUI turnInformationText;


    private List<Hero> playerHeroes = new List<Hero>();
    public Hero enemyHero;
    GameState gameState;

    public static event Action<GameState> OnBattleEnded;
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
        enemyHeroData = heroesDatabase.GetEnemyHeroDataByIndex(2);


        //Spawn
        playerHeroes = SpawnPlayerHeroes();
        enemyHero = SpawnEnemyHero(enemyHeroData);

        gameState = GameState.Playing;

    }
    public bool isBattleBusy = false;
    public bool isPlayerTurn = true;

    public void OnAttackComplete()
    {
        isBattleBusy = false;
        gameState = UpdateGameState();

        //Check State
        if (gameState == GameState.Playing)
        {
            PrepareNextBattleRound();
        }
        else
        {
            OnBattleEnded?.Invoke(gameState);
        }


    }
    private void PrepareNextBattleRound()
    {
        if (isPlayerTurn)
        {
            turnInformationText.text = "Your turn, choose a hero.";
        }
        else
        {
            isBattleBusy = true;
            turnInformationText.text = "Enemy's turn, please wait.";

            enemyHero.GetComponent<EnemyHero>().StartAttack(playerHeroes);
            isPlayerTurn = true;
        }
    }

    private GameState UpdateGameState()
    {
        // If the enemy is dead >> player wins
        if (!enemyHero.GetIsAlive())
        {
            return GameState.Win;
        }

        // Check if at least one player hero is alive
        foreach (Hero playerHero in playerHeroes)
        {
            if (playerHero.GetIsAlive())
            {
                return GameState.Playing;
            }
        }

        // If no player heroes are alive >> game over
        return GameState.GameOver;
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

    public List<Hero> GetPlayerHeroes()
    {
        return playerHeroes; 
    }
}
