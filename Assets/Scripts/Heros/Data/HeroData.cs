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
    public string id; //hero id
    public string heroName;//hero name
    public int baseHealth;//minimun hero health (base health)
    public int baseAttackPower;//minimum hero attack power (base attack power)
    public UnlockType unlockType;//is free (is unlocked) or is not free (is player reward)
    public int maxExprience;//maximum hero exprience

    



    //Return hero ID
    public string GetID() 
    {
        return id;
    }




    #region Hero Sprites
    //Hero Sprites dictionary (hero part name , hero part sprite)
    private Dictionary<string, Sprite> heroSprites;

    //Return Hero Image
    public Sprite GetHeroImage()
    {
        return LoadSprites("Hero_Image");
    }

    //Return Body Sprite
    public Sprite GetBodySprite()
    {
        return LoadSprites("Hero_Body");
    }

    //Return Hand Sprite
    public Sprite GetHandSprite() {
        return LoadSprites("Hero_Hand");
    }

    //Return Left Leg Sprite
    public Sprite GetLeftLegSprite() {
        return LoadSprites("Hero_LeftLeg");
    }

    //Return Right Leg Sprite
    public Sprite GetRightLegSprite() {
        return LoadSprites("Hero_RightLeg");
    }


    private Sprite LoadSprites(string spriteName)
    {
        if (heroSprites == null)
        {
            //Load hero body parts
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
