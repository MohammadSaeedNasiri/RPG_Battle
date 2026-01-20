using UnityEngine;
public struct HeroExprienceData
{
    public int exprience;
    public int maxExprience;

    public int level;
    public int maxLevel;

    public float attackPower;
    public float maxAttackPower;

    public float health;
    public float maxHealth;
}
public class PlayerExperienceManager : MonoBehaviour
{
    public static PlayerExperienceManager Instance;
    private const string HERO_EXPRIENCE_KEY = "HeroExprience";
    private const int EXP_PER_LEVEL = 5;

    [SerializeField]
    private HeroesDatabase heroesDatabase;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public HeroExprienceData GetHeroAllExprienceData(string heroID)
    {
        HeroExprienceData exprienceData = new HeroExprienceData();

        exprienceData.level = GetHeroLevel(heroID);
        exprienceData.maxLevel = GetHeroMaxLevel(heroID);

        exprienceData.exprience = GetHeroExprience(heroID);
        exprienceData.maxExprience = GetHeroMaxExprience(heroID);

        exprienceData.attackPower = GetHeroAttackPower(heroID);
        exprienceData.maxAttackPower = GetHeroMaxAttackPower(heroID);

        exprienceData.health = GetHeroHealth(heroID);
        exprienceData.maxHealth = GetHeroMaxHealth(heroID);

        return exprienceData;
    }



    //Hero Exprience
    public void AddHeroExprience(string heroID, int valueForAdd)
    {
        int exprience = PlayerPrefs.GetInt(HERO_EXPRIENCE_KEY + heroID, 0);
        exprience += valueForAdd;
        PlayerPrefs.SetInt(HERO_EXPRIENCE_KEY + heroID, exprience);
        PlayerPrefs.Save();
    }
    public int GetHeroExprience(string heroID)
    {
        int exprience = PlayerPrefs.GetInt(HERO_EXPRIENCE_KEY + heroID, 0);
        return exprience;
    }
    public int GetHeroMaxExprience(string heroID)
    {
        int maxExprience = heroesDatabase.GetHeroDataByID(heroID).maxExprience;
        return maxExprience;
    }


    //Hero Level
    public int GetHeroLevel(string heroID)
    {
        int exprience = GetHeroExprience(heroID);
        int level = exprience / EXP_PER_LEVEL;
        return level;
    }
    public int GetHeroMaxLevel(string heroID)
    {
        int exprience = GetHeroMaxExprience(heroID);
        int maxLevel = exprience / EXP_PER_LEVEL;
        return maxLevel;
    }

    //Hero Attack Power
    public float GetHeroAttackPower(string heroID)
    {
        int baseAttackPower = heroesDatabase.GetHeroDataByID(heroID).baseAttackPower;
        int level = GetHeroLevel(heroID);
        float attackPower = baseAttackPower + (level * (baseAttackPower * 0.1f));
        return attackPower;
    }
    public float GetHeroMaxAttackPower(string heroID)
    {
        int baseAttackPower = heroesDatabase.GetHeroDataByID(heroID).baseAttackPower;
        int maxLevel = GetHeroMaxLevel(heroID);
        float maxAttackPower = baseAttackPower + (maxLevel * (baseAttackPower * 0.1f));
        return maxAttackPower;
    }


    //Hero Health
    public float GetHeroHealth(string heroID)
    {
        int baseHealth = heroesDatabase.GetHeroDataByID(heroID).baseHealth;
        int level = GetHeroLevel(heroID);
        float health = baseHealth + (level * (baseHealth * 0.1f));
        return health;
    }
    public float GetHeroMaxHealth(string heroID)
    {
        int baseHealth = heroesDatabase.GetHeroDataByID(heroID).baseHealth;
        int maxLevel = GetHeroMaxLevel(heroID);
        float maxHealth = baseHealth + (maxLevel * (baseHealth * 0.1f));
        return maxHealth;
    }
}
