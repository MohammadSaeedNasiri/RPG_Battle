using System;
using UnityEngine;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour
{
    private HeroData heroData;

    [SerializeField]
    private Image heroImage;
    [SerializeField]
    private GameObject haveFocusFrame;
    [SerializeField]
    private GameObject lockedHero;

    public Action<HeroItem> OnSelecedHero;
    public void LoadHeroItem(HeroData heroData)
    {
        this.heroData = heroData;
        gameObject.name = heroData.id;
        heroImage.sprite = heroData.image;


        if(PlayerExprienceManager.Instance.GetPlayerPlayedWarCount() < heroData.requiredWarCount)//Check is unlocked hero?
            lockedHero.SetActive(true);
        else
            lockedHero.SetActive(false);
    }
    public void SetOnSelectedHeroEvent(Action<HeroItem> OnSelecedHero)
    {
        this.OnSelecedHero = OnSelecedHero;
    }

    public void SelectHeroItem()
    {
        haveFocusFrame.SetActive(true);
        HeroInformationViewer.Instance.ShowHeroInformations(heroData);
        OnSelecedHero?.Invoke(this);
    }
    public void DeselectHeroItem()
    {
        haveFocusFrame.SetActive(false);
    }

}
