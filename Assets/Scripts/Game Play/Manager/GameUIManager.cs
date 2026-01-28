using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Panels (Win)")]
    [SerializeField] private GameObject winPanel;

    [Header("Player Heroes")]
    [SerializeField] private Image[] heroesImage;
    [SerializeField] private Slider[] heroesExpSlider;
    [SerializeField] private TextMeshProUGUI[] heroesExpText;

    [Header("UI Panels (Lose)")]
    [SerializeField] private GameObject losePanel;


    [Header("UI Panels (Unlock new hero)")]
    [SerializeField] private GameObject unlockedNewHeroPanel;
    [SerializeField] private Image newHeroImage;

    [Header("UI Panels (Waiting)")]
    [SerializeField] private GameObject waiting;

    public void ShowWin()
    {
        if (winPanel != null)
            MenuManager.Instance.OpenMenu(winPanel);
    }

    public void ShowHeroUpgradeExpReward(int heroIndex, HeroRuntimeData heroRuntimeData, int increaseExprienceValue)
    {
        if (heroIndex < 0 || heroIndex >= heroesImage.Length)
            return;

        heroesImage[heroIndex].sprite = heroRuntimeData.heroData.GetHeroImage();
        heroesExpText[heroIndex].text = heroRuntimeData.heroExprienceData.exprience.ToString();
        heroesExpSlider[heroIndex].maxValue = heroRuntimeData.heroExprienceData.maxExprience;
        heroesExpSlider[heroIndex].value = heroRuntimeData.heroExprienceData.exprience;

        if (heroRuntimeData.isAlive && increaseExprienceValue != 0)
        {
            heroesExpText[heroIndex].text += "+" + increaseExprienceValue;
            heroesExpText[heroIndex].color = Color.yellow;
        }
        else
        {
            if (increaseExprienceValue == 0 && heroRuntimeData.isAlive)
            {
                heroesExpText[heroIndex].text = "MAX";
            }
            else
            {
                heroesImage[heroIndex].color = Color.gray;
                heroesExpText[heroIndex].text = "Dead";

            }
        }
    }


    public void ShowLose()
    {
        if (losePanel != null)
            MenuManager.Instance.OpenMenu(losePanel);
    }

    public void ShowNewHeroUnlocked(HeroData unlockedHeroData)
    {
        if (unlockedNewHeroPanel != null)
            MenuManager.Instance.OpenMenu(unlockedNewHeroPanel);

        if (newHeroImage != null)
            newHeroImage.sprite = unlockedHeroData.GetHeroImage();

    }



    public void ShowWaiting()
    {
        waiting.SetActive(true);
    }
    public void HideWaiting()
    {
        waiting.SetActive(false);
    }
}
