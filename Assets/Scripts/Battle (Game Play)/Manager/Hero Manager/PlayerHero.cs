using UnityEngine;

public class PlayerHero : Hero
{
    [Header("Hold Duration Time For Open Info Popup")]
    [SerializeField] private float holdDuration = 2f; // seconds to show hero info

    private float holdTime;
    private bool isHolding;
    private bool openInformationPopup;

    private void Update()
    {
        HandleInput();

       
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
                if (!openInformationPopup)
                    OnClicked();
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

    private void OnClicked()
    {
        if (!BattleManager.Instance.isPlayerTurn || BattleManager.Instance.GetBattleBusy())
        {
            Debug.Log("NOT player turn!!!");
            return;
        }

        if (!heroRuntimeData.isAlive) return;

        BattleManager.Instance.isPlayerTurn = false;
        BattleManager.Instance.SetBattleBusy(true);
        targetHero = BattleManager.Instance.enemyHero;
        heroMoveToTarget.SetTargetAndAttack(targetHero.transform);
    }
}
