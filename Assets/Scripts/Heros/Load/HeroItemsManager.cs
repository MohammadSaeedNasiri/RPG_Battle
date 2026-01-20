using UnityEngine;
using UnityEngine.UI;

public class HeroItemsManager : MonoBehaviour
{
    private HeroItem lastSelectedHeroItem;
    [Header("Using Hero Button")]
    [SerializeField] private Button useHeroButton;

    private void Awake()
    {
        Debug.Assert(useHeroButton != null, "UseHeroButton is not assigned");
    }

    public void HandleHeroSelections(HeroItem selectedHeroItem)
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
