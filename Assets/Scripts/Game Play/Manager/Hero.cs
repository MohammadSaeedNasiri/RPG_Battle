using System;
using UnityEngine;
[Serializable]
public enum HeroType
{
    PlayerHero,
    EnemyHero
}
[Serializable]
public struct HeroRuntimeData
{
    public HeroData heroData;
    public HeroType heroType;
    public HeroExprienceData heroExprienceData;
    
    public bool isAlive;
    public void Initialize(HeroData heroData, HeroExprienceData heroExprienceData ,HeroType heroType)
    {
        this.heroData = heroData;
        this.heroExprienceData = heroExprienceData;
        this.isAlive = true;
        this.heroType = heroType;
    }
}
public class Hero : MonoBehaviour
{
    private HeroRuntimeData heroRuntimeData;
    [SerializeField] private HeroUI heroUI;
    [SerializeField] protected HeroMoveToTarget heroMoveToTarget;
    protected Hero targetHero;

    public HeroRuntimeData GetHeroRuntimeData()
    {
        return heroRuntimeData; 
    }


    public void Initialize(HeroData heroData,HeroExprienceData heroExprienceData, HeroType heroType)
    {
        heroRuntimeData.Initialize(heroData, heroExprienceData, heroType);

        heroUI.Initialize(heroRuntimeData);
    }


   
    public void TakeDamage()
    {

    }
    public void DealDamage()
    {

    }

    public void Die()
    {

    }
}
