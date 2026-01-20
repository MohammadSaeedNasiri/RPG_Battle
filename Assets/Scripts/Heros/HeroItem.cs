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
    [SerializeField]
    private GameObject selectedHero;

    public Action<HeroItem> OnSelecedHero;

    private bool isSelectableHero = false;
    public void LoadHeroItem(HeroData heroData)
    {
        this.heroData = heroData;
        gameObject.name = heroData.heroName;
        heroImage.sprite = heroData.image;
        heroName.text = heroData.heroName;

        CheckIsSelectableHero();
    }

    private void CheckIsSelectableHero()
    {
        if (PlayerHerosManager.Instance.CheckPlayerHaveHero(heroData.id))//Check is unlocked hero?
        {
            lockedHero.SetActive(false);
            if (PlayerDeckManager.Instance.CheckIsHeroInPlayerDeck(heroData.id))
            {
                selectedHero.SetActive(true);
                isSelectableHero = false;
            }
            else
            {
                selectedHero.SetActive(false);
                isSelectableHero = true;
            }

        }
        else
        {
            lockedHero.SetActive(true);
            isSelectableHero = false;
        }
    }
    public void ReloadHeroItem()
    {
        LoadHeroItem(heroData);
    }    
    public void SetOnSelectedHeroEvent(Action<HeroItem> OnSelecedHero)
    {
        this.OnSelecedHero = OnSelecedHero;
    }

    public void SelectHeroItem()
    {
        haveFocusFrame.SetActive(true);
        CheckIsSelectableHero();
        OnSelecedHero?.Invoke(this);
    }
    public void DeselectHeroItem()
    {
        haveFocusFrame.SetActive(false);
    }

    public bool GetIsSelectableHero()
    {
        return isSelectableHero; 
    }

    public HeroData GetHeroItemData()
        { return heroData; }

    #region Open hero inforamtion popup
    //Click/Touch On Hero Item 
    bool isPointerDown = false;
    float pointerDownTimer = 0; //keep touch on item timer
    private void Update()
    {
        if (isPointerDown)
        {
            pointerDownTimer += Time.deltaTime;//Increase timer
            if (pointerDownTimer >= 2)
            {
                HeroInformationViewer.Instance.ShowHeroInformations(heroData);//Show information in popup
                pointerDownTimer = 0;
                isPointerDown = false;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDownTimer = 0;//Reset timer
        isPointerDown = false;//End
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;//Reset timer
        SelectHeroItem();
        pointerDownTimer = 0;//Start touch/click
    }
    #endregion
}
