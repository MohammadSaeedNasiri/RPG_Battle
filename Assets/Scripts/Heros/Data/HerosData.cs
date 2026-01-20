using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//For enter and save heroes 
[CreateAssetMenu(fileName = "HerosData", menuName = "Heros/HerosData")]
public class HerosData : ScriptableObject
{
   /* [Header("Heros")]
    [SerializeField]
    private List<HeroData> herosData;




    public int GetHerosCount()//Get all declared heroes for game count
    {
        return herosData.Count; 
    }

    public HeroData GetHeroData(int heroIndex)//Get all data of hero base on hero index
    {
        return herosData[heroIndex];
    }
    public HeroData GetHeroData(string heroID)//Get all data of hero base on hero ID
    {
        //HeroData heroData = herosData.Where(data => data.id == heroID).First();//select hero with enterd ID
        //return heroData;
        return herosData.FirstOrDefault(data => data.id == heroID);//select hero with enterd ID
    }

    public bool CheckIsHeroFree(string heroID)//Check is hero free to use
    {
        return GetHeroData(heroID).isFree;
    }*/

    

}
