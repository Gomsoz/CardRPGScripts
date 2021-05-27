using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_House : Scene_Base
{
    int m_houseCode;

    protected override void Init()
    {
        base.Init();

        Managers.Board.LoadBoard("H1000");
    }
}
