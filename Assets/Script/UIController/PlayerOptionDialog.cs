using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOptionDialog : MonoBehaviour {

    [SerializeField]
    private Slider SliderColorP1;
    [SerializeField]
    private Slider SliderShapeP1;
    [SerializeField]
    private Slider SliderColorP2;
    [SerializeField]
    private Slider SliderShapeP2;

    [SerializeField]
    private Tile TileP1;
    [SerializeField]
    private Tile TileP2;

    void Awake()
    {
        SliderColorP1.maxValue = SliderColorP2.maxValue = FlagProperties.Instance.AllColors.Count - 1;
        SliderShapeP1.maxValue = SliderShapeP2.maxValue = FlagProperties.Instance.AllSprites.Count - 1;
    }

    public void Reset()
    {
        SliderColorP1.value = FlagProperties.Instance.ColorIndexX;
        SliderShapeP1.value = FlagProperties.Instance.SpriteIndexX;
        SliderColorP2.value = FlagProperties.Instance.ColorIndexO;
        SliderShapeP2.value = FlagProperties.Instance.SpriteIndexO;

        TileP1.SetFlag(Flag.X);
        TileP2.SetFlag(Flag.O);
    }

    public void SetFlagColorX()
    {
        FlagProperties.Instance.SetColorFor(Flag.X, (int) SliderColorP1.value);
        TileP1.RefreshGraphic();
    }

    public void SetFlagColorO()
    {
        FlagProperties.Instance.SetColorFor(Flag.O, (int) SliderColorP2.value);
        TileP2.RefreshGraphic();
    }

    public void SetFlagShapeX()
    {
        FlagProperties.Instance.SetSpriteFor(Flag.X, (int) SliderShapeP1.value);
        TileP1.RefreshGraphic();
    }

    public void SetFlagShapeO()
    {
        FlagProperties.Instance.SetSpriteFor(Flag.O, (int) SliderShapeP2.value);
        TileP2.RefreshGraphic();
    }

    public void PlayerTilePressed(Tile tile)
    {
        if (Utility.DialogComponent != null)
        {
            Utility.DialogComponent.ReceiveOption(tile.flag);
        }
    }
}
