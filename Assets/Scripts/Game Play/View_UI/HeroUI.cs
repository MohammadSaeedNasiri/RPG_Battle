using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider healthSlider;


    public void Initialize(HeroRuntimeData hr)
    {
        nameText.text = hr.heroData.heroName;
        healthSlider.value = healthSlider.maxValue = hr.heroExprienceData.health;
       
    }
}
