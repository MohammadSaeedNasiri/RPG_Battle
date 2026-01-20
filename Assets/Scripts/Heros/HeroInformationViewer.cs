using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This class used for show hero information in popup menu (keep finger on hero for 3 seconds
public class HeroInformationViewer : MonoBehaviour
{
    public static HeroInformationViewer Instance;
    [Header("Hero Name")]
    public TextMeshProUGUI heroName;

    [Header("Hero Image")]
    public Image heroImage;

    [Header("Hero Level")]
    public TextMeshProUGUI heroLevel;
    public Slider heroLevelSlider;

    [Header("Hero Exprience")]
    public TextMeshProUGUI heroExprience;
    public Slider heroExprienceSlider;
    
    [Header("Hero Attack Power")]
    public TextMeshProUGUI heroAttackPower;
    public Slider heroAttackPowerSlider;
    
    [Header("Hero Health")]
    public TextMeshProUGUI heroHealth;
    public Slider heroHealthSlider;

    [Header("Hero Information Popup")]
    public GameObject heroInfoPopup;//Show hero informations popup menu

    [Header("Injected dependencies")]
    public HeroExprienceManager heroExprienceManager;
    public MenuManager menuManager;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Debug.Assert(heroName != null, "heroName is not assigned");
        Debug.Assert(heroImage != null, "heroImage is not assigned");
        Debug.Assert(heroLevel != null && heroLevelSlider != null, "heroLevel UI not assigned");
        Debug.Assert(heroExprience != null && heroExprienceSlider != null, "heroExprience UI not assigned");
        Debug.Assert(heroAttackPower != null && heroAttackPowerSlider != null, "heroAttackPower UI not assigned");
        Debug.Assert(heroHealth != null && heroHealthSlider != null, "heroHealth UI not assigned");
        Debug.Assert(heroInfoPopup != null, "heroInfoPopup not assigned");
    }

    public void ShowHeroInformations(HeroData heroData)
    {
        HeroExprienceData heroExprienceData = heroExprienceManager.GetHeroAllExprienceData(heroData.id);

        //Hero Name
        heroName.text = heroData.heroName;

        //Hero Image
        heroImage.sprite = heroData.image;

        //Hero Level
        ShowDataOnUI(heroLevel, heroLevelSlider, heroExprienceData.level, heroExprienceData.maxLevel);

        //Hero Exprience
        ShowDataOnUI(heroExprience, heroExprienceSlider, heroExprienceData.exprience, heroExprienceData.maxExprience);

        //Hero Attack Damage
        ShowDataOnUI(heroAttackPower, heroAttackPowerSlider, heroExprienceData.attackPower, heroExprienceData.maxAttackPower);

        //Hero Health
        ShowDataOnUI(heroHealth, heroHealthSlider, heroExprienceData.health, heroExprienceData.maxHealth);

        menuManager.OpenMenu(heroInfoPopup);

        //CheckIsUsableHero(heroData.requiredWarCount);
    }

    private void ShowDataOnUI(TextMeshProUGUI text,Slider slider ,float value,float maxValue)//Show hero parameters data
    {
        text.text = value + "/" + maxValue;
        slider.maxValue = maxValue;
        slider.value = value;
    }


}
