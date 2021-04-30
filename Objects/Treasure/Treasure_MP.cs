using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure_MP : Treasure_base
{
    // Start is called before the first frame update
    private void Start()
    {
        m_treasureType = Defines.TreasureType.StatsType;
    }

    public override void Effect()
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyAdditionalCharacterStats(StatType.MaxMP, 100);
    }
}
