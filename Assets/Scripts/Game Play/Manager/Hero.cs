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
    protected HeroRuntimeData heroRuntimeData;
    [SerializeField] private HeroUI heroUI;
    [SerializeField] protected HeroMoveToTarget heroMoveToTarget;
    [SerializeField] protected Hero targetHero;

    public HeroRuntimeData GetHeroRuntimeData()
    {
        return heroRuntimeData; 
    }


    public void Initialize(HeroData heroData,HeroExprienceData heroExprienceData, HeroType heroType)
    {
        heroRuntimeData.Initialize(heroData, heroExprienceData, heroType);

        heroUI.Initialize(heroRuntimeData);
    }


   
    public void TakeDamage(float damageValue)
    {
        heroRuntimeData.heroExprienceData.health -= damageValue;
        if(heroRuntimeData.heroExprienceData.health < 0)
        {
            heroRuntimeData.heroExprienceData.health = 0;
            Die();
        }

        heroUI.Damage(heroRuntimeData, damageValue);//Show in UI
    }
    public void DealDamage()
    {
        targetHero.TakeDamage(heroRuntimeData.heroExprienceData.attackPower);
    }

    public void Die()
    {
        heroRuntimeData.isAlive = false;
        print("Is Deaded");

    }
}
