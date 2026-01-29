using System.Collections.Generic;
using UnityEngine;
//This code is for randomly selecting a hero to be attacked by the enemy.
public class TargetSelector
{
    public Hero GetRandomAliveHero(List<Hero> playerHeroes)
    {
        if (playerHeroes == null || playerHeroes.Count == 0)
            return null;

        //Select Alive heroes
        List<Hero> aliveHeroes = new List<Hero>();
        foreach (var hero in playerHeroes)
        {
            if (hero != null && hero.GetHeroRuntimeData().isAlive)
                aliveHeroes.Add(hero);
        }

        if (aliveHeroes.Count == 0)
            return null;
        //Select random hero from alive heroes
        int randomIndex = Random.Range(0, aliveHeroes.Count);
        return aliveHeroes[randomIndex];
    }
}