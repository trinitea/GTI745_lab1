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
    private Text PanMessage;

    // Confirm / Message Dialog
    public GameObject PanConfirm;

    private Action<bool> Callback;
   
    void Start()
    {
        Callback = null;
        gameObject.SetActive(false);
    }

    public void CloseConfirm()
    {
        gameObject.SetActive(false);
    }

    public void ShowConfirmDialog(string message, Action<bool> callback = null)
    {
        Callback = callback;

        if (callback == null) BtnNo.gameObject.SetActive(false);
        else BtnNo.gameObject.SetActive(true);

        gameObject.SetActive(true);
        
        PanMessage.text = message;

    }

    public void ReceiveConfirm(bool response)
    {
        CloseConfirm();
        if (Callback != null)
        {
            Callback(response);
            Callback = null;
        }
    }
}
