using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_DrawedCardSlot : Card_SlotBehavior
{
    public override void SetSlotType()
    {
        m_slotType = Defines.CardType.Drawed;
    }

    protected override void ChangeCard(Transform card)
    {
        base.ChangeCard(card);

        GameObject go = GameObject.Instantiate(card.gameObject, transform);
        m_cardList[m_cardIdx] = go.transform;
        go.transform.position = m_emptyCardSlots[m_cardIdx].position;
        go.GetComponent<Card_Base>().SetCardInfo(m_cardIdx, m_slotType);
    }

    public void ThrowAwayAllDrawedCards()
    {
        for (int i = 0; i < m_slotCnt; i++)
        {
            if (m_cardList[i] == null)
                continue;
            GameObject tempGo = m_cardList[i].gameObject;
            FlipTheCard(i);
            GameObject.Destroy(tempGo);
        }
    }
}
