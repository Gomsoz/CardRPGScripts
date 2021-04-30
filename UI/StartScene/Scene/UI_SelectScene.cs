using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelectScene : UI_Scene
{
    enum Slot
    {
        UI_CharacterSlot_0,
        UI_CharacterSlot_1,
        UI_CharacterSlot_2,
    }

    public override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(Slot));
    }
}
