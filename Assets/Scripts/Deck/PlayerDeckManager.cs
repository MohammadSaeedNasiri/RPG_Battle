using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckManager : MonoBehaviour
{
    public static PlayerDeckManager Instance;

    //PlayerPrefs Key
    private const string DECK_CARD_KEY = "DeckCard";



    //Config Player First Deck Creating
    [SerializeField]
    [Header("Create Player First Deck ?")]
    private bool createPlayerFirstDeck = false;
    private int deckCardsCount = 3;
    [SerializeField]
    [Header("Check Is Player Deck Ready?")]
    private bool checkIsPlayerDeckReadyForStartMatch;

    //Dependency
    [Header("Heroes database")]
    //public HerosData herosData;
    public HeroesDatabase heroesDatabase;


    //UI
    [Header("UI")]
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

    #region Initialize first deck cards
    private void CreatePlayerFirstDeck()//To select three default heroes for the user's first login
    {
        if (PlayerPrefs.GetInt("IsCreatedPlayerFirstDeck", 0) == 0)
        {
            for (int i = 0; i < deckCardsCount; i++)//Set 3 free heroes for player deck cards
            {
                PlayerPrefs.SetString(DECK_CARD_KEY + i, heroesDatabase.GetHeroDataByIndex(i).id);
            }

            PlayerPrefs.SetInt("IsCreatedPlayerFirstDeck", 1);
            PlayerPrefs.Save();
        }
    }
    #endregion


    #region Load/Reload player deck cards
    private void LoadPlayerDeckCards()//Load user heroes
    {
        for (int i = 0; i < deckCardsCount; i++)
        {
            LoadPlayerDeckCard(i);//Load user heroes in player deck cards
            LoadPlayerDeckCardInLobby(i);// Load user heroes in lobby
        }
    }
    private void LoadPlayerDeckCard(int slotIndex)//Load user heroes in player deck cards
    {
        if (playerDeckCards[slotIndex] == null) return;
        string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);
        if (deckCardHeroID != string.Empty)
        {
            playerDeckCards[slotIndex].LoadDeckCard(heroesDatabase.GetHeroDataByID(deckCardHeroID));
        }
        else
        {
            playerDeckCards[slotIndex].LoadDeckCard(null);
        }
    }
    public void ReloadPlayerDeckCard(int slotIndex)//Reload user heroes
    {
        LoadPlayerDeckCard(slotIndex);//Reload user heroes in player deck cards
        LoadPlayerDeckCardInLobby(slotIndex);//Reload user heroes in lobby
    }
    private void LoadPlayerDeckCardInLobby(int slotIndex)//Load user heroes in player deck cards
    {
        if (playerDeckCardsInLobby[slotIndex] == null) return;

        string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);

        playerDeckCardsInLobby[slotIndex].gameObject.SetActive(deckCardHeroID != string.Empty);
        if (deckCardHeroID != string.Empty)
        {
            playerDeckCardsInLobby[slotIndex].sprite = heroesDatabase.GetHeroDataByID(deckCardHeroID).image;
        }
    }
    #endregion


    #region Add/Delete player deck cards
    public void SetPlayerDeckCard(int slotIndex,string heroID)//Set and save hero IDs for player deck cards
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, heroID);//Save
        ReloadPlayerDeckCard(slotIndex);//Reload changed slot
        OnChangedPlayerDeckCards();//Check is ready player deck card for start match
    }
    public void ClearPlayerDeckCard(int slotIndex)//Delete Hero From Player Deck Card
    {
        PlayerPrefs.SetString(DECK_CARD_KEY + slotIndex, string.Empty);//Clear
        ReloadPlayerDeckCard(slotIndex);//Reload changed slot
        OnChangedPlayerDeckCards();//Check is ready player deck card for start match (not ready)
    }
    #endregion

    #region Check
    public bool CheckIsHeroInPlayerDeck(string  heroID)//To check if the hero ID is present in the player's deck cards? Has the player used this hero?
    {
        bool isHeroInPlayerDeck = false;
        for (int i = 0; i < deckCardsCount; i++)//check all cards (3 cards)
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + i, string.Empty);//Get deck card hero id
            if (deckCardHeroID == heroID)//player have this hero?
            {
                isHeroInPlayerDeck = true; 
                break;
            }
        }
        return isHeroInPlayerDeck;
    }
    private void OnChangedPlayerDeckCards()//call when player deck cards changed
    {
        CheckIsPlayerDeckReadyForStartMatch();
    }
    private void CheckIsPlayerDeckReadyForStartMatch()//Check is player deck cards ready for start match (player have 3 select heroes?)
    {
        if (!checkIsPlayerDeckReadyForStartMatch)//Skip Checking
            return;

        bool isReady = true;
        for (int slotIndex = 0; slotIndex < deckCardsCount; slotIndex++)//check all deck cards
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);//get deck card hero ID
            if (deckCardHeroID == string.Empty)
            {
                isReady = false;
                break;
            }
        }
        startMatchButton.interactable = isReady;//Enable or Disable start match button

    }
    #endregion


    public List<HeroData> GetPlayerDeckCards()
    {
        List<HeroData> heroDatas = new List<HeroData>();
        for (int slotIndex = 0; slotIndex < deckCardsCount; slotIndex++)
        {
            string deckCardHeroID = PlayerPrefs.GetString(DECK_CARD_KEY + slotIndex, string.Empty);
            if (deckCardHeroID != string.Empty)
            {
                heroDatas.Add(heroesDatabase.GetHeroDataByID(deckCardHeroID));
            }
            else
            {
                heroDatas.Add(null);
            }
        }
        return heroDatas;
    }
}
