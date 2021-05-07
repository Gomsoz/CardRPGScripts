using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Attack : Card_Base
{
    public override void SetCardInfo()
    {
        CardMana = 10;
    }

    public override void Used()
    {
        base.Used();
    }

    public override void UsedWithMarkPoint(Defines.Position pos)
    {
        Transform enemy = Managers.Board.GetObjOnBoard(pos);
        if (enemy == null)
            return;

        Managers.Object.Player.GetComponent<Char_BaseCtr>().AttackOtherCharacter(enemy);
    }
}
