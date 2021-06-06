using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_House : Scene_Game
{
    public override void AwakeInit()
    {
        Managers.World.SetMapIdx("SubMap_0");
        sceneType = Defines.SceneType.HouseScene;

        Managers.Board.LoadBoard("H1000");
    }
}
