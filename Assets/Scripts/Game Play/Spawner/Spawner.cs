using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
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

    public List<GameObject> SpawnPlayerCharacters(List<HeroData> heroesData)
    {
        List<GameObject> spawnedHeroes = new List<GameObject>();
        ClearCharacters();

        int spawnCount = Mathf.Min(heroesData.Count, playerHeroesPositions.Length);

        for (int i = 0; i < spawnCount; i++)
        {
            spawnedHeroes.Add(SpawnSingleHero(heroesData[i], playerHeroesPositions[i]));
        }
        return spawnedHeroes;
    }

    public GameObject SpawnEnemy(HeroData enemyData)
    {
       // if (spawnedEnemyView != null)
          //  Destroy(spawnedEnemyView.gameObject);

        GameObject enemyObj = Instantiate(enemyHeroPrefab, heroesContainer);
        enemyObj.transform.position = enemyHeroPosition.position;

        //spawnedEnemyView = enemyObj.GetComponent<CharacterView>();
        return enemyObj;
        //spawnedEnemyView.Initialize(enemyData);
    }

    /*public List<CharacterView> GetSpawnedHeroes()
    {
        return spawnedHeroViews;
    }*/

   /* public CharacterView GetSpawnedEnemy()
    {
        return spawnedEnemyView;
    }*/

    #endregion

    #region Private Methods

    private GameObject SpawnSingleHero(HeroData heroData, Transform spawnPoint)
    {
        GameObject heroObj = Instantiate(playerHeroPrefab, heroesContainer);
        heroObj.transform.position = spawnPoint.position;

        //CharacterView characterView = heroObj.GetComponent<CharacterView>();
        //characterView.Initialize(heroData);

        //spawnedHeroViews.Add(characterView);
        return heroObj;
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
