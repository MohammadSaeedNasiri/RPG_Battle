using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private HeroData heroData;


    [Header("Dependencies")]
    private PlayerHerosManager playerHerosManager;
    private PlayerDeckManager playerDeckManager;
    private HeroInformationViewer heroInformationViewer;

    [Header("Hero Item UI")]
    [SerializeField] private TextMeshProUGUI heroName;
    [SerializeField] private Image heroImage;
    [SerializeField] private GameObject haveFocusFrame;
    [SerializeField] private GameObject lockedHero;
    [SerializeField] private GameObject selectedHero;

    //Event
    public Action<HeroItem> OnSelectedHero;

    private bool isSelectableHero = false;

    //Set Hero Dependencies with script
    public void InitDependencies(PlayerHerosManager playerHerosManager, PlayerDeckManager playerDeckManager, HeroInformationViewer heroInformationViewer)
    {
        this.playerHerosManager = playerHerosManager;
        this.playerDeckManager = playerDeckManager;
        this.heroInformationViewer = heroInformationViewer;
    }

    //return hero item data
    public HeroData GetHeroItemData()
    {
        return heroData;
    }

    //Show hero item data
    public void LoadHeroItem(HeroData heroData)
    {
        this.heroData = heroData;
        gameObject.name = heroData.heroName;
        heroImage.sprite = heroData.GetHeroImage();
        heroName.text = heroData.heroName;

        CheckIsSelectableHero();
    }

    //check is Locked Or Selected Hero
    private void CheckIsSelectableHero()
    {
        if (playerHerosManager.CheckPlayerHaveHero(heroData.id))//Check is unlocked hero?
        {
            lockedHero.SetActive(false);
            if (playerDeckManager.CheckIsHeroInPlayerDeck(heroData.id))
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

    //Reload hero item UI
    public void ReloadHeroItem()
    {
        LoadHeroItem(heroData);
    }


    #region Select Hero Item
    //Select clicked item and deselect others
    public void SetOnSelectedHeroEvent(Action<HeroItem> OnSelecedHero)
    {
        this.OnSelectedHero = OnSelecedHero;
    }

    //Select hero item
    public void SelectHeroItem()
    {
        haveFocusFrame.SetActive(true);
        CheckIsSelectableHero();
        OnSelectedHero?.Invoke(this);
    }
    //deselect hero item
    public void DeselectHeroItem()
    {
        haveFocusFrame.SetActive(false);
    }

    public bool GetIsSelectableHero()
    {
        return isSelectableHero;
    }
    #endregion

   

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
                heroInformationViewer.ShowHeroInformations(heroData);//Show information in popup
                isPointerDown = false;
            }
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerDownTimer < 2)
        {
            SelectHeroItem();
        }
        pointerDownTimer = 0;//Reset timer
        isPointerDown = false;//End
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;//Reset timer
        pointerDownTimer = 0;//Start touch/click
    }
    #endregion
}
