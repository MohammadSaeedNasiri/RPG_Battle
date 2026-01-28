using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
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

    




    public string GetID() { return id; }




    #region Hero Sprites
    private Dictionary<string, Sprite> heroSprites;

    public Sprite GetHeroImage()
    {
        return LoadSprites("Hero_Image");
    }

    public Sprite GetBodySprite()
    {
        return LoadSprites("Hero_Body");
    }
    public Sprite GetHandSprite() {
        return LoadSprites("Hero_Hand");
    }
    public Sprite GetLeftLegSprite() {
        return LoadSprites("Hero_LeftLeg");
    }
    public Sprite GetRightLegSprite() {
        return LoadSprites("Hero_RightLeg");
    }


    private Sprite LoadSprites(string spriteName)
    {
        if (heroSprites == null)
        {
            heroSprites = new Dictionary<string, Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("Heroes/Sprites/" + id);

            foreach (var s in sprites)
            {
                heroSprites.Add(s.name, s);

            }
        }

        return heroSprites[spriteName];
    }
    #endregion
}
