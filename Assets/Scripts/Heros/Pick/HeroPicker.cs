using UnityEngine;

public class HeroPicker : MonoBehaviour
{
    [SerializeField] private PlayerDeckManager playerDeckManager;
    [SerializeField] private HeroItemsManager heroItemsManager;
    [SerializeField] private HeroItemsLoader heroItemsLoader;
    [SerializeField] private GameObject selectHeroPage;

    private int selectedSlotIndex;
    public void OpenHeroMenuForSelectHero(int slotIndex)// Opens the hero selection menu for a specific deck slot.
    {
        selectedSlotIndex = slotIndex;
        MenuManager.Instance.OpenMenu(selectHeroPage);// Open the hero selection UI
        heroItemsLoader.ReloadHeroesInUIForPick(); // Refresh the hero items display
    }

    public void AssignSelectedHeroToDeckSlot()// Assigns the currently selected hero to the previously chosen deck slot.
    {
        var selectedHero = heroItemsManager.GetSelectedHeroItem();
        if (selectedHero == null) return;// Exit if no hero is selected

        playerDeckManager.SetPlayerDeckCard(selectedSlotIndex, selectedHero.GetHeroItemData().id); // Assign the selected hero to the player's deck slot
        MenuManager.Instance.CloseLastMenu();// Close the hero selection menu
        playerDeckManager.ReloadPlayerDeckCard(selectedSlotIndex);// Refresh the deck slot UI
    }

}
