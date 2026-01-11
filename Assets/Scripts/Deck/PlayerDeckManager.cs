using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    //PlayerPrefs Key
    private const string DECK_CARD_KEY = "DeckCard";



    //Config Player First Deck Creating
    public bool createPlayerFirstDeck = true;
    private int deckCardsCount = 3;
    //Dependency
    public HerosData herosData;


    //UI
    [SerializeField]
    private Image[] deckCardsImage;




    void Start()
    {
        if(createPlayerFirstDeck)
            CreatePlayerFirstDeck();

        LoadPlayerDeckCards();
    }




    private void CreatePlayerFirstDeck()
    {
        if (PlayerPrefs.GetInt("IsCreatedPlayerFirstDeck", 0) == 0)
        {
            for (int i = 0; i < deckCardsCount; i++)
            {
                PlayerPrefs.SetString(DECK_CARD_KEY + i, herosData.GetHeroData(i).id);
            }

            PlayerPrefs.SetInt("IsCreatedPlayerFirstDeck", 1);
            PlayerPrefs.Save();
        }
    }

    private void LoadPlayerDeckCards()
    {
        for (int i = 0; i < deckCardsCount; i++)
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + i, string.Empty);
            if (deckCardHeroID != string.Empty)
            {
                deckCardsImage[i].sprite = herosData.GetHeroData(deckCardHeroID).image;
            }
        }
    }
}
