using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Game States
[Serializable]
public enum GameState
{
    Playing,
    GameOver,
    Win
}

//This code is responsible for managing the game's battle.
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;

    [Header("Dependences")]
    [SerializeField] private HeroesDatabase heroesDatabase;
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    [Header("Show Current Turn In UI")]
    [SerializeField] private TextMeshProUGUI turnInformationText;



    private List<Hero> playerHeroes = new List<Hero>();
    public Hero enemyHero;
    GameState gameState;

    public static event Action<GameState> OnBattleEnded;

    public bool isPlayerTurn = true;//Is Player Turn?

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
        //Select Random Enemy 
        HeroData enemyHeroData = new HeroData();
        enemyHeroData = heroesDatabase.GetEnemyHeroDataRandomly();


        //Spawn
        playerHeroes = SpawnPlayerHeroes();//Spawn player Heroes
        enemyHero = SpawnEnemyHero(enemyHeroData);//Spawn enemy hero

        //Start game
        gameState = GameState.Playing;

    }

    #region Battle State
    private bool isBattleBusy = false;//Is Run Attack?
    public bool GetBattleBusy()
    { 
        return isBattleBusy;
    }

    public void SetBattleBusy(bool state)
    {
        isBattleBusy = state;
        if (isBattleBusy)
            gameUIManager.ShowWaiting();
        else
            gameUIManager.HideWaiting();

        turnInformationText.gameObject.SetActive(!state);
    }
    #endregion


    public void OnAttackComplete()
    {
        SetBattleBusy(false);
        gameState = UpdateGameState();//Check game is ended?

        //Check State
        if (gameState == GameState.Playing)//continue game
        {
            PrepareNextBattleRound();
        }
        else//end game
        {
            OnBattleEnded?.Invoke(gameState);
        }


    }

    //Next Round
    private void PrepareNextBattleRound()
    {
        if (isPlayerTurn)
        {
            turnInformationText.text = "Your turn, choose a hero.";
        }
        else
        {
            SetBattleBusy(true);
            turnInformationText.text = "Enemy's turn, please wait.";

            var enemyComponent = enemyHero.GetComponent<EnemyHero>();
        
            if (enemyComponent != null) enemyComponent.StartAttack(playerHeroes);
            {
                isPlayerTurn = true;
            }
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

    #region Spawn Heros
    //Spawn Heroes of player and return them
    private List<Hero> SpawnPlayerHeroes()
    {
        List<HeroData> playerDeckCards = new List<HeroData>();
        List<Hero> spawnedHeroes = new List<Hero>();

        playerDeckCards = playerDeckManager.GetPlayerDeckCards();
        spawnedHeroes = spawner.SpawnPlayerHeroes(playerDeckCards);

        return spawnedHeroes;
    }

    //Spawn Hero of Enemy and return it
    private Hero SpawnEnemyHero(HeroData heroData)
    {
        return spawner.SpawnEnemyHero(heroData);
    }
    #endregion

    //Get player heroes list in battle
    public List<Hero> GetPlayerHeroes()
    {
        return playerHeroes; 
    }
}
