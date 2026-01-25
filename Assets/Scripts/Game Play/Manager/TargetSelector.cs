using System.Collections.Generic;
using UnityEngine;

public class TargetSelector
{
    public Hero GetRandomAliveHero(List<Hero> playerHeroes)
    {
        if (playerHeroes == null || playerHeroes.Count == 0)
            return null;

        List<Hero> aliveHeroes = new List<Hero>();

        foreach (var hero in playerHeroes)
        {
            if (hero != null && hero.GetHeroRuntimeData().isAlive)
                aliveHeroes.Add(hero);
        }

        if (aliveHeroes.Count == 0)
            return null;

        int randomIndex = Random.Range(0, aliveHeroes.Count);
        return aliveHeroes[randomIndex];
    }
}