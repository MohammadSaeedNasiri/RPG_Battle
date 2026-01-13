using System;
using UnityEngine;

[Serializable]
public class HeroData
{
    [Header("Hero Config")]
    public string id;
    public string name;
    public int baseHealth;
    public int baseAttackPower;
    public bool isFree;
    public int maxExprience;

    [Header("Hero Sprites")]
    public Sprite image;
}
