using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Move : Card_Base
{
    protected override void Init()
    {
        base.Init();

        CardMana = 5;
    }
    public override void Used()
    {
        base.Used();
    }

    public override void UsedWithMarkPoint(Defines.Position pos)
    {
        Managers.Object.Player.GetComponent<Char_BaseCtr>().CharacterMove(pos);
    }
}
