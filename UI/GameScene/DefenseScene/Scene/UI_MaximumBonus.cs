using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MaximumBonus : UI_Scene
{
    [SerializeField]
    Transform m_panelMaximumBonus;

    public override void Init()
    {
        base.Init();
        GameManager.GameMgr.GamePause();
    }

    private void OnEnable()
    {
        GameManager.GameMgr.GamePause();
    }
}
