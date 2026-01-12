using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckCard : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI deckCardName;
    [SerializeField]
    private Image deckCardImage;
    [SerializeField]
    private GameObject deckCardAdd;
    [SerializeField]
    private GameObject deckCardHero;

    public void LoadDeckCard(HeroData heroData)
    {
        if (heroData != null)
        {
            deckCardName.text = heroData.name;
            deckCardImage.sprite = heroData.image;
        }

        deckCardAdd.SetActive(heroData == null);
        deckCardHero.SetActive(heroData != null);
    }
}
