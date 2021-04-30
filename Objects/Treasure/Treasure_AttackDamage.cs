using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_AttackDamage : Treasure_base
{
    private void Start()
    {
        m_treasureType = Defines.TreasureType.StatsType;
    }

    public override void Effect()
    {
        Char_CommonStats stats = new Char_CommonStats
        {
            AttackDamage = 0.1f
        };

        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyAdditionalCharacterStats(StatType.Attack, 10);
    }
}
