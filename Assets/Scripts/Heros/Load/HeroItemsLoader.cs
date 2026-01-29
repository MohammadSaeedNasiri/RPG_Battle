using System.Collections.Generic;
using UnityEngine;


//This script loading all player hero items in UI
public class HeroItemsLoader : MonoBehaviour
{
    [Header ("Dependences")]
    [SerializeField] private HeroesDatabase heroesDatabase;
    [SerializeField] private HeroItemsManager heroItemsManager;
    [SerializeField] private PlayerHerosManager playerHerosManager;
    [SerializeField] private PlayerDeckManager playerDeckManager;
    [SerializeField] private HeroInformationViewer heroInformationViewer;


    //Prefab and Containers
    [SerializeField] private GameObject heroItemsPrefab;
    [SerializeField] private Transform heroItemsContainer;

    private List<HeroItem> heroItems = new List<HeroItem>();

    void Start()
    {
        LoadHeroesInUI();
    }

    void LoadHeroesInUI()
    {
        int herosCount = heroesDatabase.PlayerHeroesCount;

        HeroData heroData;
        for (int i = 0; i < herosCount; i++)
        {
            heroData = heroesDatabase.GetPlayerHeroDataByIndex(i);
           

            var heroItemObj = Instantiate(heroItemsPrefab, heroItemsContainer);//Generate hero item
            if (!heroItemObj.TryGetComponent(out HeroItem heroItem))//Check Have HeroItem script?
            {
                Debug.LogError("HeroItem component missing on prefab");
                return;
            }

            //Set Dependencies
            heroItem.InitDependencies(playerHerosManager, playerDeckManager, heroInformationViewer);

            //Show Hero data on Hero Item
            heroItem.LoadHeroItem(heroData);

            //Set On Selected Event
            heroItem.SetOnSelectedHeroEvent(heroItemsManager.HandleHeroSelections);

            //Add to hero items list
            heroItems.Add(heroItem);
        }
    }

    public void ReloadHeroesInUIForPick()//Run With Player Decks Slot Click Button
    {
        foreach (var heroItem in heroItems)
        {
            heroItem.ReloadHeroItem();
        }
        if (heroItems.Count > 0)
        {
            heroItems[0].SelectHeroItem();//Select First Hero Item
        }
    }
}
