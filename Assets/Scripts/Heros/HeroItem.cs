using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private HeroData heroData;
    [SerializeField]
    private TextMeshProUGUI heroName;
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
        heroName.text = heroData.name;

        if (PlayerExprienceManager.Instance.GetPlayerPlayedWarCount() < heroData.requiredWarCount)//Check is unlocked hero?
            lockedHero.SetActive(true);
        else
            lockedHero.SetActive(false);
    }

    public void ReloadHeroItem()
    {

    }    
    public void SetOnSelectedHeroEvent(Action<HeroItem> OnSelecedHero)
    {
        this.OnSelecedHero = OnSelecedHero;
    }

    public void SelectHeroItem()
    {
        haveFocusFrame.SetActive(true);
        OnSelecedHero?.Invoke(this);
    }
    public void DeselectHeroItem()
    {
        haveFocusFrame.SetActive(false);
    }




    //Click On Hero Item 
    bool isPointerDown = false;
    float pointerDownTimer = 0;
    private void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;
            if (pointerDownTimer >= 2)
            {
                HeroInformationViewer.Instance.ShowHeroInformations(heroData);
                pointerDownTimer = 0;
                isPointerDown = false;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDownTimer = 0;
        isPointerDown = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        SelectHeroItem();
        pointerDownTimer = 0;
    }
}
