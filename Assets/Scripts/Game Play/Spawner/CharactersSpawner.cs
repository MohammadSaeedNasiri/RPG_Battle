using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    [Header("Characters spawn positions")]
    public Transform[] playerCharactersPosition;
    public Transform enemyCharacterPosition;

    [Header("Characters container")]
    public Transform charactersContainer;

    [Header("Characters Prefab")]
    public GameObject playerCharacterPrefab;


    public void SpawnPlayerCharaters(List<HeroData> herosData)
    {
       // List<HeroData> herosData = new List<HeroData> ();
       // herosData = PlayerDeckManager.Instance.GetPlayerDeckCards();
        int index = 0;

        foreach (var heroData in herosData)
        {
            GameObject obj = Instantiate(playerCharacterPrefab, charactersContainer);
            obj.transform.position = playerCharactersPosition[index].position;

            index++;
        }
    }
}
