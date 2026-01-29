using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Panels (Win)")]
    [SerializeField] private GameObject winPanel;// Panel shown when player wins

    [Header("Player Heroes")]
   [SerializeField] private Image[] heroesImage;// Hero images
    [SerializeField] private Slider[] heroesExpSlider;// Hero XP sliders
    [SerializeField] private TextMeshProUGUI[] heroesExpText;// Hero XP text

    [Header("UI Panels (Lose)")]
    [SerializeField] private GameObject losePanel;// Panel shown when player loses


    [Header("UI Panels (Unlock new hero)")]
    [SerializeField] private GameObject unlockedNewHeroPanel;// Panel for new hero unlock
    [SerializeField] private Image newHeroImage;// Image of unlocked hero

    [Header("UI Panels (Waiting)")]
    [SerializeField] private GameObject waiting;// Busy / waiting indicator


    #region WIN
    // Show win panel
    public void ShowWin()
    {
        if (winPanel != null)
            MenuManager.Instance.OpenMenu(winPanel);
    }

    // Update hero XP UI
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
                heroesExpText[heroIndex].text = "MAX";// Hero XP full
            }
            else
            {
                heroesExpText[heroIndex].text = "Dead";

            }
        }
    }
    #endregion

    #region Lose
    // Show lose panel
    public void ShowLose()
    {
        if (losePanel != null)
            MenuManager.Instance.OpenMenu(losePanel);
    }

    #endregion

    #region Unlock New Hero
    // Show unlocked hero panel
    public void ShowNewHeroUnlocked(HeroData unlockedHeroData)
    {
        if (unlockedNewHeroPanel != null)
            MenuManager.Instance.OpenMenu(unlockedNewHeroPanel);

        if (newHeroImage != null)
            newHeroImage.sprite = unlockedHeroData.GetHeroImage();

    }
    #endregion

    #region Busy / waiting indicator
    // Show busy/waiting panel
    public void ShowWaiting()
    {
        waiting.SetActive(true);
    }
    // Hide busy/waiting panel
    public void HideWaiting()
    {
        waiting.SetActive(false);
    }
    #endregion
}
