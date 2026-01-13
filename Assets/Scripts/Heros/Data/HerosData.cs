using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "HerosData", menuName = "Heros/HerosData")]
public class HerosData : ScriptableObject
{
    [Header("Heros")]
    [SerializeField]
    private List<HeroData> herosData;




    public int GetHerosCount()
    {
        return herosData.Count; 
    }

    public HeroData GetHeroData(int heroIndex)
    {
        return herosData[heroIndex];
    }
    public HeroData GetHeroData(string heroID)
    {
        HeroData heroData = herosData.Where(data => data.id == heroID).First();
        return heroData;
    }

    public bool CheckIsHeroFree(string heroID)
    {
        return GetHeroData(heroID).isFree;
    }

    

}
