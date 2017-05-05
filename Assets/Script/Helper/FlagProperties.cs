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

    [SerializeField]
    private Sprite SpriteX;
    [SerializeField]
    private Sprite SpriteO;

    [SerializeField]
    private Color ColorX;
    [SerializeField]
    private Color ColorO;

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
        List<Sprite> sprites = Resources.LoadAll<Sprite>("Sprite/XO_2_mask").ToList();

        if (sprites.Count > 1)
        {
            SpriteX = sprites[0];
            SpriteO = sprites[1];
        }

        ColorX = new Color(1.00f, 1.00f, 0.00f); // Yellow
        ColorO = new Color(0.00f, 1.00f, 1.00f); // Cyan
    }

    public Color GetColorFor(Flag flag)
    {
        Color color = Color.white;

        switch (flag)
        {
            case Flag.X:
                color = ColorX;
                break;
            case Flag.O:
                color = ColorO;
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
                sprite = SpriteX;
                break;
            case Flag.O:
                sprite = SpriteO;
                break;
        }

        return sprite;
    }
}
