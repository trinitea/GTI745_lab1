using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour {

    public Flag flag { get; private set; }
    
    private Image image;
    private Button button;
    private Mask imageMask;

    // Use this for initialization
    void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponentsInChildren<Image>()[1]; // first one being the button image
        imageMask = GetComponentInChildren<Mask>();

        if (button == null || image == null || imageMask == null)
        {
            Debug.LogError("Tile required components are missing");
            return;
        }

        flag = Flag.None;
    }

    public void SetFlag(Flag newFlag)
    {
        flag = newFlag;
        RefreshGraphic();
    }

    public void RefreshGraphic()
    {
        if (flag != Flag.None)
        {

            imageMask.showMaskGraphic = true;
            image.sprite = FlagProperties.Instance.GetSpriteFor(flag);
            image.color = FlagProperties.Instance.GetColorFor(flag);

        }
        else
        {
            imageMask.showMaskGraphic = false;
        }
    }
}
