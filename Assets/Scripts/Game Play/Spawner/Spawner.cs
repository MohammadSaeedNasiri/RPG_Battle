using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private HeroExprienceManager heroExprienceManager;

    [Header("Heroes spawn positions")]
    [SerializeField] private Transform[] playerHeroesPositions;
    [SerializeField] private Transform enemyHeroPosition;

    [Header("Heroes container")]
    [SerializeField] private Transform heroesContainer;

    [Header("Heroes Prefab")]
    [SerializeField] private GameObject playerHeroPrefab;
    [SerializeField] private GameObject enemyHeroPrefab;

    //private readonly List<CharacterView> spawnedHeroViews = new List<CharacterView>();
    //private CharacterView spawnedEnemyView;

    #region Public API

    public List<Hero> SpawnPlayerHeroes(List<HeroData> heroesData)
    {
        List<Hero> spawnedHeroes = new List<Hero>();
       // ClearCharacters();

        int spawnCount = Mathf.Min(heroesData.Count, playerHeroesPositions.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            spawnedHeroes.Add(SpawnSingleHero(heroesData[i], playerHeroesPositions[i]));
        }
        return spawnedHeroes;
    }

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

    private Hero SpawnSingleHero(HeroData heroData, Transform spawnPoint)
    {
        GameObject heroObj = Instantiate(playerHeroPrefab, heroesContainer);
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

    private void ClearCharacters()
    {
       // foreach (Transform child in heroesContainer)
        //{
          //  Destroy(child.gameObject);
        //}

        //spawnedHeroViews.Clear();
        //spawnedEnemyView = null;
    }

    #endregion
}
