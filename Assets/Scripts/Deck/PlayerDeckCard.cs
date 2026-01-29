using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Player Deck Card Item
public class PlayerDeckCard : MonoBehaviour//, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI deckCardName;//hero name
    [SerializeField] private Image deckCardImage;//hero image
    [SerializeField] private GameObject deckCardAdd;//add hero to deck button
    [SerializeField] private GameObject deckCardHero;//show hero card

    #region Loading deck cards
    //Load player deck cards and show on UI
    public void LoadDeckCard(HeroData heroData)
    {
        if (heroData != null)//if not slot empty
        {
            deckCardName.text = heroData.heroName;
            deckCardImage.sprite = heroData.GetHeroImage();
        }

        deckCardAdd.SetActive(heroData == null);//if user dont have hero on deck slot , show Add card button
        deckCardHero.SetActive(heroData != null);//if user have hero on deck slot , show it.
    }
    #endregion

}

