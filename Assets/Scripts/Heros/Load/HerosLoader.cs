using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerosLoader : MonoBehaviour
{
    //Dependency
    public HerosData herosData;

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
        for (int i = 0; i < herosCount;i++)
        {
            heroData = herosData.GetHeroData(i);

            GameObject heroItem = Instantiate(heroItemsPrefab, heroItemsContainer);//Generate hero item
            heroItem.GetComponent<HeroItem>().LoadHeroItem(heroData);
            
            
        }


    }

    /*void HeroItemSetData()
    {

    }*/
}
