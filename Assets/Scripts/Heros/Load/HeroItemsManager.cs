using UnityEngine;

public class HeroItemsManager : MonoBehaviour
{
    private HeroItem lastSelectedHeroItem;

    public void OnSelectedHeroItem(HeroItem selectedHeroItem)
    {
        if (lastSelectedHeroItem != null)
        {
            lastSelectedHeroItem.DeselectHeroItem();
        }
        lastSelectedHeroItem = selectedHeroItem;
    }
}
