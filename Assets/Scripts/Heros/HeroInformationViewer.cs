using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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


    [Header("Using Hero Button")]
    public Button useHeroButton;

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
        heroName.text = heroData.name;

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


        CheckIsUsableHero(heroData.requiredWarCount);
    }

    private void ShowDataOnUI(TextMeshProUGUI text,Slider slider ,float value,float maxValue)
    {
        text.text = value + "/" + maxValue;
        slider.maxValue = maxValue;
        slider.value = value;
    }


    private void CheckIsUsableHero(int requiredWarCount)
    {
        int playerPlayedWarCount = PlayerExprienceManager.Instance.GetPlayerPlayedWarCount();
        if (playerPlayedWarCount >= requiredWarCount)
        {
            useHeroButton.interactable = true;
        }
        else
        {
            useHeroButton.interactable = false;
        }
    }
}
