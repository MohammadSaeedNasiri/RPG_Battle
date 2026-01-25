using UnityEngine;

public class PlayerHero : Hero
{
    private float holdTime;
    private bool isHolding;
    private bool openInformationPopup;

    private void OnMouseDown()
    {
        holdTime = 0f;
        isHolding = true;
        openInformationPopup = false;
    }

    private void OnMouseUp()
    {

        if (!openInformationPopup)
        {
            OnClicked();
        }

        isHolding = false;
    }

    private void Update()
    {
        if (!isHolding || openInformationPopup)
            return;

        holdTime += Time.deltaTime;

        if (holdTime >= 2f)
        {
            openInformationPopup = true;
        }
    }


    private void OnClicked()
    {
        if (BattleManager.isPlayerTurn)
        {
            BattleManager.isPlayerTurn = false;
            BattleManager.isBattleBusy = true;
            heroMoveToTarget.SetTarget(BattleManager.activeEnemyHero.transform);

        }else
        {
            Debug.Log("NOT player turn!!!");
        }
        
    }
}
