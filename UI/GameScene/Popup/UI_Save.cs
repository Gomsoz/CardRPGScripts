using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Save : UI_Popup
{
    enum Btns
    {
        Btn_Play,
        Btn_Quit,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Btns));

        AddUIHandler(Get<Button>((int)Btns.Btn_Play).gameObject, ClickPlayBtn);
        AddUIHandler(Get<Button>((int)Btns.Btn_Quit).gameObject, ClickQuitBtn);
    }

    public void ClickPlayBtn(PointerEventData evt)
    {
        GameManager.GameMgr.GamePause();
        Managers.UI.ClosePopupUI();
    }

    public void ClickQuitBtn(PointerEventData evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
