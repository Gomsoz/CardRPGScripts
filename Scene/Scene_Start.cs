using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Start : Scene_Base
{
    protected override void Init()
    {
        base.Init();
        sceneType = Defines.SceneType.StartScene;

        LoadUI();
    }

    void LoadUI()
    {
        Managers.UI.ShowSceneUI<UI_StartScene>(Defines.SceneType.StartScene);
        Managers.UI.ShowSceneUI<UI_SelectScene>(Defines.SceneType.StartScene).gameObject.SetActive(false);
    }

    public override void Clear()
    {
        base.Clear();
    }
}
