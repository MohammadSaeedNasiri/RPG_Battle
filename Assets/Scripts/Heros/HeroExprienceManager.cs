using UnityEngine;


public struct HeroExprienceData
{
    public int heroExprience;
    public int heroMaxExprience;

    public int heroLevel;
    public int heroMaxLevel;

    public float heroAttackPower;
    public float heroMaxAttackPower;

    public float heroHealth;
    public float heroMaxHealth;
}
public class HeroExprienceManager : MonoBehaviour
{
    public static HeroExprienceManager Instance;
    private const string HERO_EXPRIENCE = "HeroExprience";

    [SerializeField]
    private HerosData herosData;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else
        {
            Destroy(gameObject);
        }
    }

    public HeroExprienceData GetHeroAllExprienceData(string heroID)
    {
        HeroExprienceData heroExprienceData = new HeroExprienceData();

        heroExprienceData.heroLevel = GetHeroLevel(heroID);
        heroExprienceData.heroMaxLevel = GetHeroMaxLevel(heroID);

        heroExprienceData.heroExprience = GetHeroExprience(heroID);
        heroExprienceData.heroMaxExprience = GetHeroMaxExprience(heroID);

        heroExprienceData.heroAttackPower = GetHeroAttackPower(heroID);
        heroExprienceData.heroMaxAttackPower = GetHeroMaxAttackPower(heroID);

        heroExprienceData.heroHealth = GetHeroHealth(heroID);
        heroExprienceData.heroMaxHealth = GetHeroMaxHealth(heroID);

        return heroExprienceData;
    }



    //Hero Exprience
    public void AddHeroExprience(string heroID, int valueForAdd)
    {
        int heroExprience = PlayerPrefs.GetInt(HERO_EXPRIENCE + heroID, 0);
        heroExprience += valueForAdd;
        PlayerPrefs.SetInt(heroID, heroExprience);
        PlayerPrefs.Save();
    }
    public int GetHeroExprience(string heroID)
    {
        int heroExprience = PlayerPrefs.GetInt(heroID, 0);
        return heroExprience;
    }
    public int GetHeroMaxExprience(string heroID)
    {
        int heroMaxExprience = herosData.GetHeroData(heroID).maxExprience;
        return heroMaxExprience;
    }


    //Hero Level
    public int GetHeroLevel(string heroID)
    {
        int heroExprience = GetHeroExprience(heroID);
        int heroLevel = heroExprience / 5;
        return heroLevel;
    }
    public int GetHeroMaxLevel(string heroID)
    {
        int heroExprience = GetHeroMaxExprience(heroID);
        int heroMaxLevel = heroExprience / 5;
        return heroMaxLevel;
    }

    //Hero Attack Power
    public float GetHeroAttackPower(string heroID)
    {
        int heroBaseAttackPowerExprience = herosData.GetHeroData(heroID).baseAttackPower;
        int heroLevel = GetHeroLevel(heroID);
        float heroAttackPower = heroBaseAttackPowerExprience + (heroLevel * (heroBaseAttackPowerExprience * 0.1f)); 
        return heroAttackPower;
    }
    public float GetHeroMaxAttackPower(string heroID)
    {
        int heroBaseAttackPowerExprience = herosData.GetHeroData(heroID).baseAttackPower;
        int heroMaxLevel = GetHeroMaxLevel(heroID);
        float heroMaxAttackPower = heroBaseAttackPowerExprience + (heroMaxLevel * (heroBaseAttackPowerExprience * 0.1f));
        return heroMaxAttackPower;
    }


    //Hero Health
    public float GetHeroHealth(string heroID)
    {
        int heroBaseHealth = herosData.GetHeroData(heroID).baseHealth;
        int heroLevel = GetHeroLevel(heroID);
        float heroHealth = heroBaseHealth + (heroLevel * (heroBaseHealth * 0.1f));
        return heroHealth;
    }
    public float GetHeroMaxHealth(string heroID)
    {
        int heroBaseHealth = herosData.GetHeroData(heroID).baseHealth;
        int heroMaxLevel = GetHeroMaxLevel(heroID);
        float heroMaxHealth = heroBaseHealth + (heroMaxLevel * (heroBaseHealth * 0.1f));
        return heroMaxHealth;
    }
}
