using UnityEngine;

public class HeroPicker : MonoBehaviour
{
    public GameObject selectHeroPage;
    private int selectedSlotIndex;
    public void OpenHeroMenuForSelectHero(int slotIndex)
    {
        selectedSlotIndex = slotIndex;
        MenuManager.Instance.OpenMenu(selectHeroPage);
    }


}
