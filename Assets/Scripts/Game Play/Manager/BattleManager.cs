using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField]private PlayerDeckManager playerDeckManager;
    [SerializeField] private Spawner spawner;


    private List<GameObject> playerHeroes = new List<GameObject>();
    private GameObject enemyHero;
    private void Start()
    {
        SetupGame();
    }


    private void SetupGame()
    {
        //Enemy Hero Data (static for test)
        HeroData enemyHeroData = new HeroData();



        //Spawn
        SpawnPlayerHeroes();
        SpawnEnemyHero(enemyHeroData);
    }

    private void SpawnPlayerHeroes()
    {
        List<HeroData> playerDeckCards = new List<HeroData>();
        playerDeckCards = playerDeckManager.GetPlayerDeckCards();
        playerHeroes = spawner.SpawnPlayerCharacters(playerDeckCards);
    }

    private void SpawnEnemyHero(HeroData heroData)
    {
        enemyHero = spawner.SpawnEnemy(heroData);
    }
}
