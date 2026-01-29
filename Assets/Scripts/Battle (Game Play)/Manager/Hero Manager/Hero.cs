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

    // Initialize hero runtime data
    public void Initialize(HeroData heroData, HeroExprienceData heroExprienceData, HeroType heroType)
    {
        this.heroData = heroData;
        this.heroExprienceData = heroExprienceData;
        this.isAlive = true;
        this.heroType = heroType;
    }
}

public class Hero : MonoBehaviour
{
    [Header("Dependences")]
    [SerializeField] private HeroUI heroUI;
    [SerializeField] protected HeroMoveToTarget heroMoveToTarget;
    [SerializeField] protected HeroAnimationManager heroAnimationManager;

    protected HeroRuntimeData heroRuntimeData;
    protected Hero targetHero;

    // Get hero runtime data
    public HeroRuntimeData GetHeroRuntimeData()
    {
        return heroRuntimeData;
    }

    // Initialize hero
    public void Initialize(HeroData heroData, HeroExprienceData heroExprienceData, HeroType heroType)
    {
        heroRuntimeData.Initialize(heroData, heroExprienceData, heroType);
        if (heroUI != null) heroUI.Initialize(heroRuntimeData); // init UI
    }

    // Take damage and update UI
    public void TakeDamage(float damageValue)
    {
        heroRuntimeData.heroExprienceData.health -= damageValue;
        if (heroRuntimeData.heroExprienceData.health < 0)
        {
            heroRuntimeData.heroExprienceData.health = 0;
            Die(); // die if health <= 0
        }

        if (heroUI != null) heroUI.Damage(heroRuntimeData, damageValue);
    }

    // Deal damage to target hero
    public void DealDamage()
    {
        targetHero.TakeDamage(heroRuntimeData.heroExprienceData.attackPower);
    }

    // Set hero as dead and play animation
    public void Die()
    {
        heroRuntimeData.isAlive = false;
        if (heroAnimationManager != null) heroAnimationManager.PlayDie();
    }

    // Check if hero is alive
    public bool GetIsAlive()
    {
        return heroRuntimeData.isAlive;
    }
}
