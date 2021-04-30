using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_SlotBehavior : MonoBehaviour
{
    [SerializeField]
    protected Transform[] m_emptyCardSlots;
    protected Transform[] m_cardList;
    protected bool[] m_isEmptyCard;

    protected Defines.CardType m_slotType;

    protected int m_cardIdx;
    
    [SerializeField]
    protected int m_slotCnt;
    public int SlotCnt { get { return m_slotCnt; } }

    private void Awake()
    {
        m_slotCnt = gameObject.transform.childCount;
        m_emptyCardSlots = new Transform[m_slotCnt];
        m_cardList = new Transform[m_slotCnt];
        m_isEmptyCard = new bool[m_slotCnt];

        for(int i = 0; i < m_slotCnt; i++)
        {
            m_emptyCardSlots[i] = Utils.FindChild<Transform>(gameObject, $"CardSlot_{i}", true);
        }
    }
    public virtual void SetSlotType()
    {
    }

    public Transform GetCardInList(int idx)
    {
        return m_cardList[idx]; 
    }

    public bool CheckAndChangeCard(Transform card)
    {
        if (IsEmptySlot() == false)
            return false;
        ChangeCard(card);
        return true;
    }

    protected virtual void ChangeCard(Transform card)
    {
        m_isEmptyCard[m_cardIdx] = true;
        m_emptyCardSlots[m_cardIdx].rotation = Quaternion.Euler(new Vector3(0, 90, 0));
    }

    public void FlipTheCard(int idx)
    {      
        m_cardList[idx] = null;
        m_isEmptyCard[idx] = false;
        m_emptyCardSlots[idx].rotation = Quaternion.Euler(new Vector3(0, 0, 0));
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
