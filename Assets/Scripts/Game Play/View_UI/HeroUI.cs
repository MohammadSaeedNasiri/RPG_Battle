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


    [Header("Hero Body")]
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer hand;
    [SerializeField] private SpriteRenderer leftLeg;
    [SerializeField] private SpriteRenderer rightLeg;

    public void Initialize(HeroRuntimeData hr)
    {
        healthSlider.maxValue = hr.heroExprienceData.health;

        UpdateUI(hr);
       if (hr.heroType == HeroType.EnemyHero)
            RevertHeroDirection();//Change Hero Direction
        else
            LoadHeroSkinOnBody(hr.heroData);
    }



    private void LoadHeroSkinOnBody(HeroData heroData)
    {
        body.sprite = heroData.GetBodySprite();
        hand.sprite = heroData.GetHandSprite();
        leftLeg.sprite = heroData.GetLeftLegSprite();
        rightLeg.sprite = heroData.GetRightLegSprite();

    }



    public void Damage(HeroRuntimeData hr,float damageValue)
    {
        UpdateUI(hr);
        ShowDamageAnim(damageValue);

        if (hr.heroExprienceData.health > 0)
        {
            StartCoroutine(PopCoroutine(1.1f, 0.2f));
        }
        else
        {
            ShowDieAnim();
        }
    }


    private void UpdateUI(HeroRuntimeData hr)
    {
        nameText.text = hr.heroData.heroName;
        healthSlider.value = hr.heroExprienceData.health;
    }

    private void ShowDamageAnim(float damageValue)
    {
        damageAnimatedText.text = damageValue.ToString();
        // damageAnimatedText.gameObject.SetActive(true);
        StartCoroutine(PlayAnimationCoroutine(damageAnimatedText.GetComponent<Animator>()));

    }
    private IEnumerator PlayAnimationCoroutine(Animator animator)
    {
        animator.SetBool("Play", true);

        yield return new WaitForSeconds(0.02f);
        

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length - 0.2f);

        animator.SetBool("Play", false);
    }
    private IEnumerator PopCoroutine(float scaleAmount, float duration)
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scaleAmount;

        
        transform.localScale = target;

        yield return new WaitForSeconds(duration / 2f);

        //heroImage.color = Color.white;
        transform.localScale = original;
    }
    private void ShowDieAnim()
    {
       // heroImage.color = Color.gray;
        nameText.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
    }

    [SerializeField] private Transform bodyContainer;
    public void RevertHeroDirection()
    {
        Vector3 scale = bodyContainer.localScale;
        scale.x *= -1;
        bodyContainer.localScale = scale;
    }
}

