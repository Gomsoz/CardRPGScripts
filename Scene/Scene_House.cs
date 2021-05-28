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

        Managers.Object.Player.GetComponent<Char_PlayerCtr>().SetPosition(new Defines.Position(2, 3));

        GameObject mark = Managers.Resources.Instantiate("Prefabs/Object/Mark/PortalMark");
        mark.transform.position = Managers.Board.BoardPosToWorldPos(new Defines.Position(1, 3));
    }
}
