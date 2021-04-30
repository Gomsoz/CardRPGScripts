using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_Armor : Treasure_base
{
    private void Start()
    {
        m_treasureType = Defines.TreasureType.StatsType;
    }

    public override void Effect()
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyAdditionalCharacterStats(StatType.Armor, 5);
    }
}
