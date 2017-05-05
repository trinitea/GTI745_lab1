using System;
using UnityEngine;
using UnityEngine.UI;

public class DialogHelper : MonoBehaviour
{
    [SerializeField]
    private Button BtnYes;
    [SerializeField]
    private Button BtnNo;

    [SerializeField]

    // Confirm / Message Dialog
    private Text ConfirmMessage;
    public GameObject PanConfirm;

    // Player Option Dialog
    public PlayerOptionDialog PanPlayerOption;

    // Possible Callbacks
    private Action<bool> CallbackBool;
    private Action<Flag> CallbackFlag;

    #region Confirm Dialog
    public void CloseConfirm()
    {
        gameObject.SetActive(false);
        PanConfirm.SetActive(false);
    }

    public void ShowConfirmDialog(string message, Action<bool> callback = null)
    {
        CallbackBool = callback;

        if (CallbackBool == null) BtnNo.gameObject.SetActive(false);
        else BtnNo.gameObject.SetActive(true);

        gameObject.SetActive(true);
        PanConfirm.SetActive(true);

        ConfirmMessage.text = message;

    }

    public void ReceiveConfirm(bool response)
    {
        CloseConfirm();
        if (CallbackBool != null)
        {
            CallbackBool(response);
            CallbackBool = null;
        }
    }
    #endregion

    #region Player Option Dialog

    public void ClosePlayerOption()
    {
        gameObject.SetActive(false);
        PanPlayerOption.gameObject.SetActive(false);
    }

    public void ShowPlayerOptionDialog(Action<Flag> callback = null)
    {
        if (callback == null)
        {
            return;
        }

        CallbackFlag = callback;

        gameObject.SetActive(true);
        PanPlayerOption.gameObject.SetActive(true);
        PanPlayerOption.Reset();
    }

    public void ReceiveOption(Flag flag)
    {
        ClosePlayerOption();
        if (CallbackFlag != null)
        {
            CallbackFlag(flag);
            CallbackFlag = null;
        }
    }

    #endregion
}
