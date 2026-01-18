using UnityEngine;
using UnityEngine.UI;

public class HeroItemsManager : MonoBehaviour
{
    private HeroItem lastSelectedHeroItem;
    [Header("Using Hero Button")]
    public Button useHeroButton;
    public void OnSelectedHeroItem(HeroItem selectedHeroItem)
    {
        //if (lastSelectedHeroItem == selectedHeroItem) return;
        if (lastSelectedHeroItem != null && lastSelectedHeroItem != selectedHeroItem)
        {
            lastSelectedHeroItem.DeselectHeroItem();

        }
        lastSelectedHeroItem = selectedHeroItem;

        useHeroButton.interactable = selectedHeroItem.GetIsSelectableHero();
    }

    public HeroItem GetSelectedHeroItem()
    {
        return lastSelectedHeroItem; 
    }
}
