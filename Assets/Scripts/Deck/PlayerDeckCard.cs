using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerDeckCard : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private TextMeshProUGUI deckCardName;
    [SerializeField]
    private Image deckCardImage;
    [SerializeField]
    private GameObject deckCardAdd;
    [SerializeField]
    private GameObject deckCardHero;

    private HeroData heroData;
    public void LoadDeckCard(HeroData heroData)//Load deck card - show informations
    {
        if (heroData != null)
        {
            this.heroData = heroData;
            if (deckCardName != null)  deckCardName.text = heroData.name;
            if (deckCardImage != null) deckCardImage.sprite = heroData.image;
        }

        if (deckCardAdd != null) deckCardAdd.SetActive(heroData == null);
        if (deckCardHero != null) deckCardHero.SetActive(heroData != null);
    }

    #region Open hero inforamtion popup
    //Click/Touch On Hero Item 
    public bool isPointerDown = false;
    public float pointerDownTimer = 0; //keep touch on item timer
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
        pointerDownTimer = 0;//Reset timer
        isPointerDown = true;//Start touch/click
    }
    #endregion
}
