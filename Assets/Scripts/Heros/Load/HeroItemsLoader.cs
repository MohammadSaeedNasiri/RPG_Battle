using UnityEngine;

public class HeroItemsLoader : MonoBehaviour
{
    //Dependency
    public HerosData herosData;
    public HeroItemsManager heroItemsManager;

    //Prefab and Containers
    public GameObject heroItemsPrefab;
    public Transform heroItemsContainer;



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
        }
    }
}
