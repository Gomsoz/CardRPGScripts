using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_SelectScene2 : UI_Popup
{
    enum Btns
    {
        ContinueBtn,
        CancelBtn,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Btns));

        AddUIHandler(Get<Button>((int)Btns.ContinueBtn).gameObject, ClickContinueBtn);
    }

    public void ClickContinueBtn(PointerEventData evt)
    {
        GameManager.GameMgr.SaveSlotIdx = 0;
        GameManager.GameMgr.IsLoadData = true;
        Managers.Scene.LoadScene(Defines.SceneType.WaitingScene);
    }

    public void ClickCancelBtn(PointerEventData evt)
    {
        Managers.UI.ClosePopupUI();
    }
}
