using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Debug.Log("UISTART");
        Managers.UI.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
