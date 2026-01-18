using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image[] playerDeckCardsInLobby;
    [SerializeField]
    private Button startMatchButton;


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
        CheckIsPlayerDeckReadyForStartMatch();

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
            LoadPlayerDeckCardInLobby(i);
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

    public void ReloadPlayerDeckCard(int slotIndex)
    {
        LoadPlayerDeckCard(slotIndex);
        LoadPlayerDeckCardInLobby(slotIndex);
    }
    private void LoadPlayerDeckCardInLobby(int slotIndex)
    {

        string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);

        playerDeckCardsInLobby[slotIndex].gameObject.SetActive(deckCardHeroID != string.Empty);
        if (deckCardHeroID != string.Empty)
        {
            playerDeckCardsInLobby[slotIndex].sprite = herosData.GetHeroData(deckCardHeroID).image;
        }
    }
    public void SetPlayerDeckCard(int slotIndex,string heroID)
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, heroID);
        ReloadPlayerDeckCard(slotIndex);
        OnChangedPlayerDeckCards();
    }



    public void ClearPlayerDeckCard(int slotIndex)//Delete Hero From Player Deck Card
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, string.Empty);
        ReloadPlayerDeckCard(slotIndex);
        OnChangedPlayerDeckCards();
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

    private void OnChangedPlayerDeckCards()
    {
        CheckIsPlayerDeckReadyForStartMatch();
    }
    private void CheckIsPlayerDeckReadyForStartMatch()
    {
        bool isReady = true;
        for (int slotIndex = 0; slotIndex < deckCardsCount; slotIndex++)
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);
            if (deckCardHeroID == string.Empty)
            {
                isReady = false;
                break;
            }
        }
        startMatchButton.interactable = isReady;

    }
}
