using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This code is implemented to manage the user's hero selection system for the game.
public class PlayerDeckManager : MonoBehaviour
{
    //PlayerPrefs Key
    private const string DECK_CARD_KEY = "DeckCard";
    private const string FIRST_DECK_CREATED_KEY = "IsCreatedPlayerFirstDeck";

    //Config Player First Deck Creating
    [Header("Create Player First Deck")]
    [Tooltip("If CreatePlayerFirstDeck is enabled, on the player's first login, three free heroes are selected as the three player cards, otherwise the user's cards remain empty.")]
    [SerializeField] private bool createPlayerFirstDeck = false;

    [Header("Player Deck Cards Count")]
    [SerializeField] private int deckCardsCount = 3;

    [Header("Check Is Player Deck Ready?")]
    [Tooltip("With each change to the deck cards, check to see if it is ready to start the game.")]
    [SerializeField] private bool autoCheckDeckReady = true;

    //Dependency
    [Header("Heroes database")]
    public HeroesDatabase heroesDatabase;

    //UI
    [Header("UI")]
    [SerializeField] private PlayerDeckCard[] playerDeckCards;
    [SerializeField] private Image[] playerDeckCardsInLobby;//Show Player Deck Cards in Lobby
    [SerializeField] private Button startBattleButton;//Start Battle Button (Load Game Play Scene)



    void Start()
    {
        if(createPlayerFirstDeck)
            CreatePlayerFirstDeck();

        LoadPlayerDeckCards();
        CheckDeckReady();

    }

    #region Initialize first deck cards
    private void CreatePlayerFirstDeck()//To select three default heroes for the user's first login
    {
        if (PlayerPrefs.GetInt(FIRST_DECK_CREATED_KEY, 0) == 0)
        {
            for (int i = 0; i < deckCardsCount; i++)//Set 3 free heroes for player deck cards
            {
                PlayerPrefs.SetString(DECK_CARD_KEY + i, heroesDatabase.GetPlayerHeroDataByIndex(i).id);
            }

            PlayerPrefs.SetInt(FIRST_DECK_CREATED_KEY, 1);
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
        if (slotIndex < 0 || slotIndex >= playerDeckCards.Length)
            return;

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
            playerDeckCardsInLobby[slotIndex].sprite = heroesDatabase.GetHeroDataByID(deckCardHeroID).GetHeroImage();
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
        CheckDeckReady();
    }
    private void CheckDeckReady()//Check is player deck cards ready for start match (player have 3 select heroes?)
    {
        if (!autoCheckDeckReady)//Skip Checking
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
        startBattleButton.interactable = isReady;//Enable or Disable start match button

    }
    #endregion

    #region Get Player Heroes
    public List<HeroData> GetPlayerDeckCards()//Return Selected User Heroes For Battle
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
    #endregion
}
