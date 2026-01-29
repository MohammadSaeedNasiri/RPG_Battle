using System.Collections.Generic;
using UnityEngine;

public class EnemyHero : Hero
{
    [Header("Hold Duration Time For Open Info Popup")]
    [SerializeField] private float holdDuration = 2f; // seconds to show hero info

    private float holdTime;
    private bool isHolding;
    private bool openInformationPopup;

    private void Awake()
    {
        // Ensure the HeroMoveToTarget component is assigned
        if (heroMoveToTarget == null)
            heroMoveToTarget = GetComponent<HeroMoveToTarget>();
    }

    #region Attack
    public void StartAttack(List<Hero> playerHeroes)
    {
        // Select a target hero
        targetHero = SelectTarget(playerHeroes);

        // Set target and start moving/attacking
        heroMoveToTarget.SetTargetAndAttack(targetHero.transform);
    }

    private Hero SelectTarget(List<Hero> playerHeroes)
    {
        TargetSelector targetSelector = new TargetSelector();
        return targetSelector.GetRandomAliveHero(playerHeroes); // Pick a random alive hero
    }
    #endregion

    #region Info Popup
    private void Update()
    {
        HandleInput();

        // Handle long press to show hero info
        if (isHolding && !openInformationPopup)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= holdDuration)
            {
                openInformationPopup = true;
                HeroInformationViewer.Instance.ShowHeroInformations(heroRuntimeData.heroData);
            }
        }
    }

    private void HandleInput()
    {
        Vector2 inputPos = Vector2.zero;
        bool began = false;
        bool ended = false;

        // Mouse input
        if (Input.GetMouseButtonDown(0))
        {
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            began = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ended = true;
        }

        // Touch input (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            inputPos = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began) began = true;
            else if (touch.phase == TouchPhase.Ended) ended = true;
        }

        // Check if the input is over this 2D collider
        Collider2D hit = Physics2D.OverlapPoint(inputPos);
        if (hit != null && hit.gameObject == gameObject)
        {
            if (began)
            {
                holdTime = 0f;
                isHolding = true;
                openInformationPopup = false;
            }
            else if (ended)
            {
                if (openInformationPopup)
                {
                    HeroInformationViewer.Instance.ShowHeroInformations(heroRuntimeData.heroData);
                }
                isHolding = false;
            }
        }
        else
        {
            // Stop holding if input ended outside the object
            if (ended)
                isHolding = false;
        }
    }
    #endregion
}
