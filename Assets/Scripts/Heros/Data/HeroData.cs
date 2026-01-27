using System;
using UnityEngine;
public enum UnlockType
{
    DefaultUnlocked,   //Free
    ProgressBased//With player exprience
}

//Heroes data structure
[Serializable]
public class HeroData
{
    [Header("Hero Config")]
    public string id;
    public string heroName;
    public int baseHealth;//minimun hero health (base health)
    public int baseAttackPower;//minimum hero attack power (base attack power)
    public UnlockType unlockType;//is free (is unlocked) or is not free (is player reward)
    public int maxExprience;//maximum hero exprience

    [Header("Hero Sprites")]
    public Sprite image;//Hero image for show in UI

    public Sprite GetHeroImage()
        { return image; }
    public string GetID()
    {
        return id;
    }
}
