using UnityEngine;

public class HeroPicker : MonoBehaviour
{
    [SerializeField]
    private PlayerDeckManager playerDeckManager;
    [SerializeField]
    private HeroItemsManager heroItemsManager;
    [SerializeField]
    private HeroItemsLoader heroItemsLoader;
    [SerializeField]
    private GameObject selectHeroPage;

    private int selectedSlotIndex;
    public void OpenHeroMenuForSelectHero(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        MenuManager.Instance.OpenMenu(selectHeroPage);
        heroItemsLoader.ReloadHeroesInUIForPick();
    }

    public void UseSelectedHeroForPlayerDeck()
    {
        playerDeckManager.SetPlayerDeckCard(selectedSlotIndex, heroItemsManager.GetSelectedHeroItem().GetHeroItemData().id);
        MenuManager.Instance.CloseLastMenu();
        playerDeckManager.ReloadPlayerDeckCard(selectedSlotIndex);
    }

}
