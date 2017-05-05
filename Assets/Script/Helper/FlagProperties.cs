using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Flag
{
    None = 0,
    X,
    O
}

public class FlagProperties : MonoBehaviour {

    // breaks encapsulation rules !!!
    public int SpriteIndexX;
    public int SpriteIndexO;

    public int ColorIndexX;
    public int ColorIndexO;

    public List<Sprite> AllSprites;
    public List<Color> AllColors;

    private static FlagProperties instance = null;

    public static FlagProperties Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (FlagProperties)FindObjectOfType(typeof(FlagProperties));
                if (instance == null)
                {
                    instance = (new GameObject("FlagProperties")).AddComponent<FlagProperties>();
                    instance.InitDefault();
                }
            }
            return instance;

        }
    }

    private void InitDefault()
    {
        // Default
        AllSprites = Resources.LoadAll<Sprite>("Sprite/XO_2_mask").ToList();

        AllColors.Add(new Color(1.00f, 1.00f, 0.00f)); // Yellow
        AllColors.Add(new Color(0.00f, 1.00f, 1.00f)); // Cyan

        if (AllSprites.Count > 1 && AllColors.Count > 1)
        {
            SpriteIndexX = ColorIndexX = 0;
            SpriteIndexO = ColorIndexO = 1;
        }
    }

    public Color GetColorFor(Flag flag)
    {
        Color color = Color.white;

        switch (flag)
        {
            case Flag.X:
                color = AllColors[ColorIndexX];
                break;
            case Flag.O:
                color = AllColors[ColorIndexO];
                break;
        }

        return color;
    }

    public Sprite GetSpriteFor(Flag flag)
    {
        Sprite sprite = null;

        switch (flag)
        {
            case Flag.X:
                sprite = AllSprites[SpriteIndexX];
                break;
            case Flag.O:
                sprite = AllSprites[SpriteIndexO];
                break;
        }

        return sprite;
    }

    public void SetSpriteFor(Flag flag, int index)
    {
        if (index < 0 || index > AllSprites.Count)
        {
            return;
        }

        switch(flag)
        {
            case Flag.X:
                SpriteIndexX = index;
                break;

            case Flag.O:
                SpriteIndexO = index;
                break;
        }
    }

    public void SetColorFor(Flag flag, int index)
    {
        if (index < 0 || index > AllColors.Count)
        {
            return;
        }

        switch (flag)
        {
            case Flag.X:
                ColorIndexX = index;
                break;

            case Flag.O:
                ColorIndexO = index;
                break;
        }
    }
}
