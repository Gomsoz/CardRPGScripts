using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_MpPotion : Item_Base
{
    public override void Use()
    {
        Char_PlayerCtr player = Managers.Object.Player.GetComponent<Char_PlayerCtr>();

        int recoveryMP = (int)(player.PlayerStats.MaxMP * 0.1f);
        player.ModifyDefaultCharacterStats(StatType.MP, recoveryMP);
        Debug.Log($"마나를 {recoveryMP} 회복했습니다. : {player.PlayerStats.MP}");
    }

    protected override void Init()
    {
        m_itemName = "Mp Potion";
        m_itemExplain = "마나를 회복합니다.";
    }
}
