using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroItemsLoader : MonoBehaviour
{
    //Dependency
    public HerosData herosData;
    public HeroItemsManager heroItemsManager;

    //Prefab and Containers
    public GameObject heroItemsPrefab;
    public Transform heroItemsContainer;

    private List<HeroItem> heroItems = new List<HeroItem>();

    void Start()
    {
        LoadHerosInUI();
    }

    void LoadHerosInUI()
    {
        int herosCount = herosData.GetHerosCount();

        HeroData heroData;
        for (int i = 0; i < herosCount; i++)
        {
            heroData = herosData.GetHeroData(i);
            HeroItem heroItem = Instantiate(heroItemsPrefab, heroItemsContainer).GetComponent<HeroItem>();//Generate hero item
            heroItem.LoadHeroItem(heroData);
            heroItem.SetOnSelectedHeroEvent(heroItemsManager.OnSelectedHeroItem);
            heroItems.Add(heroItem);
        }
    }

    public void ReloadHerosInUIForPick()//Run With Player Decks Slot Click Button
    {
        foreach (var heroItem in heroItems)
        {
            heroItem.ReloadHeroItem();
        }
        heroItems.First().SelectHeroItem();//Select First Hero Item
    }
}
