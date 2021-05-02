using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_DrawedCardSlot : Card_SlotBehavior
{

    protected override void AwakeInit()
    {
        m_slotCnt = 5;
        m_slotType = Defines.CardType.Drawed;
    }

    public void ThrowAwayAllDrawedCards()
    {
        for (int i = 0; i < m_slotCnt; i++)
        {
            if (m_cardList[i] == null)
                continue;
            FlipTheCard(i);
        }
    }
}
