using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card_SlotBehavior
{
    protected Card_Base[] m_cardList;
    public Card_Base[] CardList { get { return m_cardList; } }

    protected bool[] m_isEmptyCard;

    protected Defines.CardType m_slotType;

    protected int m_cardIdx;
    
    [SerializeField]
    protected int m_slotCnt;
    public int SlotCnt { get { return m_slotCnt; } }

    public void CardSlotInit()
    {
        AwakeInit();

        m_cardList = new Card_Base[m_slotCnt];
        m_isEmptyCard = new bool[m_slotCnt];
    }

    protected abstract void AwakeInit();

    public bool CheckAndChangeCard(Transform card)
    {
        if (IsEmptySlot() == false)
            return false;
        ChangeCard(card);
        return true;
    }

    protected virtual void ChangeCard(Transform card)
    {
        m_cardList[m_cardIdx] = card.GetComponent<Card_Base>();
        m_isEmptyCard[m_cardIdx] = true;
        Managers.UI.GetSceneUI<GameSceneUI>().ChangeCardImage(m_slotType, m_cardIdx, card.GetComponent<SpriteRenderer>().sprite);
    }

    public void FlipTheCard(int idx)
    {      
        Managers.UI.GetSceneUI<GameSceneUI>().ChangeDefaultCardImage(m_slotType, idx);

        m_cardList[idx] = null;
        m_isEmptyCard[idx] = false;
    }
    
    bool IsEmptySlot()
    {
        m_cardIdx = 0;
        while (m_isEmptyCard[m_cardIdx] == true)
        {
            m_cardIdx++;
            if (m_cardIdx >= m_slotCnt)
                return false;
        }
        return true;
    }
}
