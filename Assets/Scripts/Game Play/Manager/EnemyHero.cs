using System.Collections.Generic;
using UnityEngine;

public class EnemyHero : Hero
{ 
    private void Awake()
    {
        if (heroMoveToTarget == null)
            heroMoveToTarget = GetComponent<HeroMoveToTarget>();
    }

    public void StartAttack(List<Hero> playerHeroes)
    {
        //Select target
        targetHero = SelectTarget(playerHeroes);
        targetHero.name = "Selected";

        //Set target and move to it
        heroMoveToTarget.SetTarget(targetHero.transform);
    }

    private Hero SelectTarget(List<Hero> playerHeroes)
    {
        TargetSelector targetSelector = new TargetSelector();
        return targetSelector.GetRandomAliveHero(playerHeroes);
    }




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

        if (openInformationPopup)
        {
            HeroInformationViewer.Instance.ShowHeroInformations(heroRuntimeData.heroData);
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
            HeroInformationViewer.Instance.ShowHeroInformations(heroRuntimeData.heroData);

        }
    }

}
