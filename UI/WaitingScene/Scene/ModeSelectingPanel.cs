using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ModeSelectingPanel : UI_Base
{
    enum Modes
    {
        Adventure_SelectMode_Normal,
        Adventure_SelectMode_Defense,
        Adventure_SelectMode_Locked,
        Count,
    }

    public override void Init()
    {
        Bind<Button>(typeof(Modes));

        AddUIHandler(Get<Button>((int)Modes.Adventure_SelectMode_Defense).gameObject, ClickDefenseMode);
    }

    public void ClickDefenseMode(PointerEventData evt)
    {
        GameManager.CurGameMode = GameMode.Defense;
        Managers.Scene.LoadScene(Defines.SceneType.DefenseScene);
    }
}
