﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_StartScene : UI_Scene
{
    enum Transforms
    {
        StartSceneBtnPanel,
    }

    enum Btns
    {
        StartBtn,
        FigBtn,
        ExitBtn,
    }

    private void Start()
    {

        Transform gridPanel = Get<Transform>((int)Transforms.StartSceneBtnPanel);
    }

    public override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(Transforms));
        
        Bind<Button>(typeof(Btns));
        AddUIHandler(Get<Button>((int)Btns.StartBtn).gameObject, ClickedStartBtn);
        AddUIHandler(Get<Button>((int)Btns.FigBtn).gameObject, ClickedFigBtn);
        AddUIHandler(Get<Button>((int)Btns.ExitBtn).gameObject, ClickedExitBtn);

    }

    public void ClickedStartBtn(PointerEventData evt)
    {
        Managers.UI.GetSceneUI<UI_SelectScene>().gameObject.SetActive(true);
        gameObject.SetActive(false);

    }

    public void ClickedFigBtn(PointerEventData evt)
    {
        Debug.Log("FIGURE");
    }

    public void ClickedExitBtn(PointerEventData evt)
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
