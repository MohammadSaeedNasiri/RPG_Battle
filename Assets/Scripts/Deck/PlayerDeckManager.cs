using UnityEngine;

public class PlayerDeckManager : MonoBehaviour
{
    public static PlayerDeckManager Instance;

    //PlayerPrefs Key
    private const string DECK_CARD_KEY = "DeckCard";



    //Config Player First Deck Creating
    public bool createPlayerFirstDeck = false;
    private int deckCardsCount = 3;
    //Dependency
    public HerosData herosData;


    //UI
    [SerializeField]
    private PlayerDeckCard[] playerDeckCards;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

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
            LoadPlayerDeckCard(i);
        }
    }
    private void LoadPlayerDeckCard(int slotIndex)
    {
        string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);
        if (deckCardHeroID != string.Empty)
        {
            playerDeckCards[slotIndex].LoadDeckCard(herosData.GetHeroData(deckCardHeroID));
        }
        else
        {
            playerDeckCards[slotIndex].LoadDeckCard(null);

        }
    }
    public void SetPlayerDeckCard(int slotIndex,string heroID)
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, heroID);
        LoadPlayerDeckCard(slotIndex);
    }
    public void ClearPlayerDeckCard(int slotIndex)
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, string.Empty);
        LoadPlayerDeckCard(slotIndex);
    }

    public bool CheckIsHeroInPlayerDeck(string  heroID)
    {
        bool isHeroInPlayerDeck = false;
        for (int i = 0; i < deckCardsCount; i++)
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + i, string.Empty);
            if (deckCardHeroID == heroID)
            {
                isHeroInPlayerDeck = true; 
                break;
            }
        }
        return isHeroInPlayerDeck;
    }
}
