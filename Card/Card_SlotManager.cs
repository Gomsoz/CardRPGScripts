using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Card_SlotManager : MonoBehaviour
{
    Card_DrawedCardSlot m_drawedCardSlot;
    Card_EnrolledCardSlot m_enrolledCardSlot;

    Card_Inventory m_playerCardInventory;

    private void Awake()
    {
        m_drawedCardSlot = new Card_DrawedCardSlot();
        m_drawedCardSlot.CardSlotInit();

        m_enrolledCardSlot = new Card_EnrolledCardSlot();
        m_enrolledCardSlot.CardSlotInit();

        m_playerCardInventory = transform.GetComponent<Card_Inventory>();
        GameManager.GameMgr.CardTimeState += CardTimeListener;

    }

    private void Start()
    {
        GameManager.TimeCardSystem.StartCardTime(Defines.CardTimeState.SelectingCard);
        DrawAllCard();
    }

    public void CardTimeListener(Defines.CardTimeState state)
    {
        switch (state)
        {
            case Defines.CardTimeState.SelectingCard:
                m_drawedCardSlot.ThrowAwayAllDrawedCards();
                m_enrolledCardSlot.UseEnrolledCard(0);
                break;
            case Defines.CardTimeState.FirstCard:
                GameManager.MarkPoint.RemoveMarkPoint();
                m_enrolledCardSlot.UseEnrolledCard(1);
                break;
            case Defines.CardTimeState.SecondCard:
                GameManager.MarkPoint.RemoveMarkPoint();
                m_enrolledCardSlot.UseEnrolledCard(2);
                break;
            case Defines.CardTimeState.ThirdCard:
                GameManager.MarkPoint.RemoveMarkPoint();
                DrawAllCard();
                GameManager.TimeCardSystem.StartCardTime(Defines.CardTimeState.SelectingCard);
                break;
        }
    }

    public void DrawAllCard()
    {
        StartCoroutine(DrawCardWithTime());
    }

    IEnumerator DrawCardWithTime()
    {
        for (int i = 0; i < m_drawedCardSlot.SlotCnt; i++)
        {
            yield return new WaitForSeconds(0.5f);
            m_drawedCardSlot.CheckAndChangeCard(m_playerCardInventory.GetCard());
        }
    }

    /*public void SelectTheCard(Transform slot)
    {
        Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero, 0f);
        if (hit.collider == null)
        {
            return;
        }

        if (hit.collider.transform.CompareTag("Card"))
        {
            int cardIdx = hit.collider.GetComponent<Card_Base>().IndexOnSlot;
            if(m_enrolledCardSlot.CheckAndChangeCard(hit.collider.transform))
                m_drawedCardSlot.FlipTheCard(cardIdx);
        }
    }*/

    public void SelectTheCard(CardSlotInfo slot)
    {
        if (m_enrolledCardSlot.CheckAndChangeCard(m_drawedCardSlot.CardList[slot.SlotIdx].transform))
            m_drawedCardSlot.FlipTheCard(slot.SlotIdx);
    }
}
