using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroUI : MonoBehaviour
{
    [Header("Hero UI")]
    [SerializeField] private TextMeshProUGUI nameText; //Hero name
    [SerializeField] private Slider healthSlider;//Hero health slider
    [SerializeField] private TextMeshProUGUI damageAnimatedText; // Hero Damage 


    [Header("Hero Body")]
    [SerializeField] private Transform bodyContainer;
    [SerializeField] private SpriteRenderer body;
    [SerializeField] private SpriteRenderer hand;
    [SerializeField] private SpriteRenderer leftLeg;
    [SerializeField] private SpriteRenderer rightLeg;

    public void Initialize(HeroRuntimeData hr)
    {
       healthSlider.maxValue = hr.heroExprienceData.health;//set slider MAX

       UpdateUI(hr);

        //Change Hero Direction (right to left) for Right Enemy 
       if (hr.heroType == HeroType.EnemyHero)
            RevertHeroDirection();//Change Hero Direction
 
       LoadHeroSkinOnBody(hr.heroData);//Load And Show Hero skin
    }


    //Show hero body skin
    private void LoadHeroSkinOnBody(HeroData heroData)
    {
        body.sprite = heroData.GetBodySprite();
        hand.sprite = heroData.GetHandSprite();
        leftLeg.sprite = heroData.GetLeftLegSprite();
        rightLeg.sprite = heroData.GetRightLegSprite();

    }


    //Show damage effect And Play Die Anim
    public void Damage(HeroRuntimeData hr,float damageValue)
    {
        UpdateUI(hr);
        ShowDamageAnim(damageValue);//Show damage text anim

        if (hr.heroExprienceData.health > 0)//is Alive?
        {
            StartCoroutine(PopCoroutine(1.1f, 0.2f));//Pulse Effect
        }
        else
        {
            ShowDieAnim();//Die Anim
        }
    }

    //Show hero info Top of hero
    private void UpdateUI(HeroRuntimeData hr)
    {
        nameText.text = hr.heroData.heroName;
        healthSlider.value = hr.heroExprienceData.health;
    }

    //Show Damage Text
    private void ShowDamageAnim(float damageValue)
    {
        damageAnimatedText.text = damageValue.ToString();
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


    //Pulse Effect
    private IEnumerator PopCoroutine(float scaleAmount, float duration)
    {
        Vector3 original = transform.localScale;
        Vector3 target = original * scaleAmount;

        
        transform.localScale = target;

        yield return new WaitForSeconds(duration / 2f);

        transform.localScale = original;
    }

    //Die
    private void ShowDieAnim()
    {
        nameText.gameObject.SetActive(false);
        healthSlider.gameObject.SetActive(false);
    }


    //Reverse hero direction
    public void RevertHeroDirection()
    {
        Vector3 scale = bodyContainer.localScale;
        scale.x *= -1;
        bodyContainer.localScale = scale;
    }
}

