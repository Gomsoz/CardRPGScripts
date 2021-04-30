using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_HpPotion : Item_Base
{   

    public override void Use()
    {
        Char_PlayerCtr player = Managers.Object.Player.GetComponent<Char_PlayerCtr>();

        int recoveryHP = (int)(player.PlayerStats.MaxHP * 0.1f);
        player.ModifyDefaultCharacterStats(StatType.HP, recoveryHP);
        Debug.Log($"체력을 {recoveryHP} 회복했습니다. : {player.PlayerStats.HP}");
    }

    protected override void Init()
    {
        m_itemName = "Hp Potion";
        m_itemExplain = "체력을 회복합니다.";
    }
}
