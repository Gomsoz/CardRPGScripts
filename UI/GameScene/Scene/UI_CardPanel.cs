using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_CardPanel : UI_Base
{
    enum Slots
    {
        DrawedCardSlot_0,
        DrawedCardSlot_1,
        DrawedCardSlot_2,
        DrawedCardSlot_3,
        DrawedCardSlot_4,
        EnrolledCardSlot_0,
        EnrolledCardSlot_1,
        EnrolledCardSlot_2,
        Count,
    }

    Sprite m_defaultCardImage;

    int m_firstIdxAtDrawed = 0;
    int m_fisrtIdxAtEnrolled = 5;

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        m_defaultCardImage = Managers.Resources.Load<Sprite>("Sprites/Card/back_wood");
        Bind<Transform>(typeof(Slots));

        for(int i = 0; i < (int)Slots.Count; i++)
        {
            AddUIHandler(Get<Transform>(i).gameObject, ClickCard);
        }

    }

    public void ChangeDrawedCardImage(int idx, Sprite image)
    {
        Get<Transform>(m_firstIdxAtDrawed + idx).GetComponent<Image>().sprite = image;
    }

    public void FlipTheDrawedCard(int idx)
    {
        Get<Transform>(m_firstIdxAtDrawed + idx).GetComponent<Image>().sprite = m_defaultCardImage;
    }

    public void ChangeEnrolledCardImage(int idx, Sprite image)
    {
        Get<Transform>(m_fisrtIdxAtEnrolled + idx).GetComponent<Image>().sprite = image;
    }

    public void FlipTheEnrolledCard(int idx)
    {
        Get<Transform>(m_fisrtIdxAtEnrolled + idx).GetComponent<Image>().sprite = m_defaultCardImage;
    }

    public void ClickCard(PointerEventData evt)
    {
        GameObject go = evt.pointerCurrentRaycast.gameObject;

        Debug.Log($"Selected Slot {go.name}");

        Managers.Slot.SelectTheCard(go.GetComponent<CardSlotInfo>());
    }
}
