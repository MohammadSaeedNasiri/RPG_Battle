using System.Collections.Generic;
using UnityEngine;

public class HeroItemsLoader : MonoBehaviour
{
    //Dependency
    //public HerosData herosData;
    public HeroesDatabase heroesDatabase;
    public HeroItemsManager heroItemsManager;

    //Prefab and Containers
    public GameObject heroItemsPrefab;
    public Transform heroItemsContainer;

    private List<HeroItem> heroItems = new List<HeroItem>();
    void Awake()
    {
        Debug.Assert(heroesDatabase != null, "HeroesDatabase is not assigned");
        Debug.Assert(heroItemsManager != null, "HeroItemsManager is not assigned");
        Debug.Assert(heroItemsPrefab != null, "HeroItemsPrefab is not assigned");
        Debug.Assert(heroItemsContainer != null, "HeroItemsContainer is not assigned");
    }
    void Start()
    {
        LoadHeroesInUI();
    }

    void LoadHeroesInUI()
    {
        int herosCount = heroesDatabase.Count;

        HeroData heroData;
        for (int i = 0; i < herosCount; i++)
        {
            heroData = heroesDatabase.GetHeroDataByIndex(i);
           

            var heroItemObj = Instantiate(heroItemsPrefab, heroItemsContainer);//Generate hero item
            if (!heroItemObj.TryGetComponent(out HeroItem heroItem))//Check Have HeroItem script?
            {
                Debug.LogError("HeroItem component missing on prefab");
                return;
            }
            //HeroItem heroItem = heroItemObj.GetComponent<HeroItem>();//Get HeroItem component
            heroItem.LoadHeroItem(heroData);
            heroItem.SetOnSelectedHeroEvent(heroItemsManager.OnSelectedHeroItem);
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
