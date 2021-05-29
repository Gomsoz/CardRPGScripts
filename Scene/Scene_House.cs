using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_House : Scene_Base
{
    int m_houseCode;

    protected override void Init()
    {
        Managers.Board.LoadBoard("H1000");

        base.Init();

        Managers.Object.Player.GetComponent<Char_PlayerCtr>().SetPosition(new Defines.Position(m_mapData.SpawnPosX, m_mapData.SpawnPosY));



        
    }
}
