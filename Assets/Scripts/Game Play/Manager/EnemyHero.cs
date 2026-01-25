using System.Collections.Generic;

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
}
