using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, false);
    }
}
