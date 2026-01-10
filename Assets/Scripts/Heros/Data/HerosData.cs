using UnityEngine;


[CreateAssetMenu(fileName = "HerosData", menuName = "Heros/HerosData")]
public class HerosData : ScriptableObject
{
    [Header("Heros")]
    [SerializeField]
    private HeroData[] heros;




    public int GetHerosCount()
    {
        return heros.Length; 
    }

    public HeroData GetHeroData(int heroIndex)
    {
        return heros[heroIndex];
    }

}
