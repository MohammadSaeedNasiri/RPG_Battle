using System.Collections.Generic;
using UnityEngine;

//This code using fotr spawn heroes
public class Spawner : MonoBehaviour
{
    [Header("Depenency")]
    [SerializeField] private HeroExprienceManager heroExprienceManager;

    [Header("Heroes spawn positions")]
    [SerializeField] private Transform[] playerHeroesPositions;//player heroes position
    [SerializeField] private Transform enemyHeroPosition;//enemy hero position

    [Header("Heroes container")]
    [SerializeField] private Transform heroesContainer;//spawned heroes container

    [Header("Heroes Prefab")]
    [SerializeField] private GameObject playerHeroPrefab;//prefab of player heroes
    [SerializeField] private GameObject enemyHeroPrefab;//prefab of enemy heroes


    #region Public API

    //Spawn player heroes with heroesData list (player all heroes)
    public List<Hero> SpawnPlayerHeroes(List<HeroData> heroesData)
    {
        List<Hero> spawnedHeroes = new List<Hero>();

        int spawnCount = Mathf.Min(heroesData.Count, playerHeroesPositions.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            spawnedHeroes.Add(SpawnSingleHero(heroesData[i], playerHeroesPositions[i]));
        }
        return spawnedHeroes;
    }

    //Spwan enemy hero with heroData
    public Hero SpawnEnemyHero(HeroData enemyData)
    {
        GameObject heroObj = Instantiate(enemyHeroPrefab, heroesContainer);
        heroObj.transform.position = enemyHeroPosition.position;

        Hero spawnedHero = heroObj.GetComponent<Hero>();
        HeroExprienceData heroExprienceData = new HeroExprienceData();
        if (spawnedHero != null)
        {
            heroExprienceData = heroExprienceManager.GetHeroAllExprienceData(enemyData.id);
            spawnedHero.Initialize(enemyData, heroExprienceData, HeroType.EnemyHero);
        }

        return spawnedHero;

    }
    #endregion

    #region Private Methods

    //Spwan ONE hero
    private Hero SpawnSingleHero(HeroData heroData, Transform spawnPoint)
    {
        GameObject heroObj = Instantiate(playerHeroPrefab, heroesContainer);
        if (heroData == null)
        {
            Debug.LogError("YOU DONT HAVE 3 SELECTED HERO!!!");
            return null;
        }
        heroObj.name = heroData.heroName;
        heroObj.transform.position = spawnPoint.position;

        Hero spawnedHero = heroObj.GetComponent<Hero>();
        HeroExprienceData heroExprienceData = new HeroExprienceData();
        if (spawnedHero != null)
        {
            heroExprienceData = heroExprienceManager.GetHeroAllExprienceData(heroData.id);
            spawnedHero.Initialize(heroData, heroExprienceData, HeroType.PlayerHero);
        }
        return spawnedHero;
    }
    #endregion
}
