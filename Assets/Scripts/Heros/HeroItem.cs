using UnityEngine;
using UnityEngine.UI;

public class HeroItem : MonoBehaviour
{
    private HeroData heroData;

    [SerializeField]
    private Image heroImage;
    [SerializeField]
    private GameObject haveFocusFrame;

    public void LoadHeroItem(HeroData heroData)
    {
        this.heroData = heroData;
        gameObject.name = heroData.id;
        heroImage.sprite = heroData.image;
    }

    public void SetFocusHeroItem()
    {
        haveFocusFrame.SetActive(true);
    }


}
