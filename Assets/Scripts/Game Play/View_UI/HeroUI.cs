using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI damageAnimatedText;



    public void Initialize(HeroRuntimeData hr)
    {
        healthSlider.maxValue = hr.heroExprienceData.health;

        UpdateUI(hr);
       if (hr.heroType == HeroType.EnemyHero)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Damage(HeroRuntimeData hr,int damageValue)
    {
        UpdateUI(hr);
        ShowDamageAnim(damageValue);
    }


    private void UpdateUI(HeroRuntimeData hr)
    {
        nameText.text = hr.heroData.heroName;
        healthSlider.value = hr.heroExprienceData.health;
    }

    private void ShowDamageAnim(int damageValue)
    {
        damageAnimatedText.text = damageValue.ToString();
        StartCoroutine(PlayAnimationCoroutine(damageAnimatedText.GetComponent<Animator>()));
       // damageAnimatedText.gameObject.SetActive(true);
    }
    private IEnumerator PlayAnimationCoroutine(Animator animator)
    {
        animator.SetBool("Play", true);

        yield return new WaitForSeconds(0.05f);

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);

        animator.SetBool("Play", false);
    }
}
