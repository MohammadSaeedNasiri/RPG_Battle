using UnityEngine;
using UnityEngine.UI;

public class HeroItemsManager : MonoBehaviour
{
    private HeroItem lastSelectedHeroItem;
    [Header("Using Hero Button")]
    [SerializeField] private Button useHeroButton;


    //When select a hero item , deselect other selected hero item
    public void HandleHeroSelections(HeroItem selectedHeroItem)
    {

        if (lastSelectedHeroItem != null && lastSelectedHeroItem != selectedHeroItem)
        {
            lastSelectedHeroItem.DeselectHeroItem();
        }

        lastSelectedHeroItem = selectedHeroItem;

        useHeroButton.interactable = selectedHeroItem.GetIsSelectableHero();
    }

    //return selected hero item
    public HeroItem GetSelectedHeroItem()
    {
        return lastSelectedHeroItem; 
    }
}
