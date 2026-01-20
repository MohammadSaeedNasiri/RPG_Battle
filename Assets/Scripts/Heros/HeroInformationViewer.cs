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

   

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void ShowHeroInformations(HeroData heroData)
    {
        HeroExprienceData heroExprienceData = HeroExprienceManager.Instance.GetHeroAllExprienceData(heroData.id);

        //Hero Name
        heroName.text = heroData.heroName;

        //Hero Image
        heroImage.sprite = heroData.image;

        //Hero Level
        ShowDataOnUI(heroLevel, heroLevelSlider, heroExprienceData.heroLevel, heroExprienceData.heroMaxLevel);

        //Hero Exprience
        ShowDataOnUI(heroExprience, heroExprienceSlider, heroExprienceData.heroExprience, heroExprienceData.heroMaxExprience);

        //Hero Attack Damage
        ShowDataOnUI(heroAttackPower, heroAttackPowerSlider, heroExprienceData.heroAttackPower, heroExprienceData.heroMaxAttackPower);

        //Hero Health
        ShowDataOnUI(heroHealth, heroHealthSlider, heroExprienceData.heroHealth, heroExprienceData.heroMaxHealth);

        MenuManager.Instance.OpenMenu(heroInfoPopup);

        //CheckIsUsableHero(heroData.requiredWarCount);
    }

    private void ShowDataOnUI(TextMeshProUGUI text,Slider slider ,float value,float maxValue)//Show hero parameters data
    {
        text.text = value + "/" + maxValue;
        slider.maxValue = maxValue;
        slider.value = value;
    }


}
