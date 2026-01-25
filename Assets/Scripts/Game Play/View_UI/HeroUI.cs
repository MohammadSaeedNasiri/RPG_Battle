using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private SpriteRenderer heroImage;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI damageAnimatedText;



    public void Initialize(HeroRuntimeData hr)
    {
        healthSlider.maxValue = hr.heroExprienceData.health;

        UpdateUI(hr);
       if (hr.heroType == HeroType.EnemyHero)
            GetComponent<SpriteRenderer>().flipX = true;
    }

    public void Damage(HeroRuntimeData hr,float damageValue)
    {
        UpdateUI(hr);
        ShowDamageAnim(damageValue);
    }


    private void UpdateUI(HeroRuntimeData hr)
    {
        nameText.text = hr.heroData.heroName;
        healthSlider.value = hr.heroExprienceData.health;
    }

    private void ShowDamageAnim(float damageValue)
    {
        damageAnimatedText.text = damageValue.ToString();
        StartCoroutine(PlayAnimationCoroutine(damageAnimatedText.GetComponent<Animator>()));
       // damageAnimatedText.gameObject.SetActive(true);
    }
    private IEnumerator PlayAnimationCoroutine(Animator animator)
    {
        animator.SetBool("Play", true);
        StartCoroutine(PopCoroutine(1.1f, 0.2f));

        yield return new WaitForSeconds(0.02f);
        

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length - 0.2f);

        animator.SetBool("Play", false);
    }
    private IEnumerator PopCoroutine(float scaleAmount, float duration)
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scaleAmount;

        heroImage.color = Color.red;
        transform.localScale = target;

        yield return new WaitForSeconds(duration / 2f);

        heroImage.color = Color.white;
        transform.localScale = original;
    }
    public void ShowDieAnim()
    {
        heroImage.color = Color.gray;

    }
}
