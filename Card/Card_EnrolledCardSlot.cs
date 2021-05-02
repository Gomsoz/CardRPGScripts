using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_EnrolledCardSlot : Card_SlotBehavior
{

    protected override void AwakeInit()
    {
        m_slotCnt = 3;
        m_slotType = Defines.CardType.Enrolled;
    }

    protected override void ChangeCard(Transform card)
    {
        float _playerMP = Managers.Object.Player.GetComponent<Char_PlayerCtr>().PlayerStats.MP;
        int _cardMP = card.GetComponent<Card_Base>().CardMana;

        Debug.Log($"플레이어 마나 : {_playerMP}, 필요 마나 : {_cardMP}");

        if (_playerMP < _cardMP)
        {
            Debug.Log("마나 부족");
            return;
        }

        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyDefaultCharacterStats(StatType.MP, -_cardMP);
        Debug.Log($"현재 플레이어 마나 : {Managers.Object.Player.GetComponent<Char_PlayerCtr>().PlayerStats.MP}");

        base.ChangeCard(card);
    }

    public void UseEnrolledCard(int idx)
    {
        if (m_cardList[idx] == null)
            return;
        m_cardList[idx].GetComponent<Card_Base>().Used();

        FlipTheCard(idx);
    }
}
