using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Time_CardSystem : MonoBehaviour
{
    int m_leftCardTime = 0;
    int[] m_FixedcardTime = new int[(int)Defines.CardTimeState.Count]
    {
        5,4,3,2
    };
    Defines.CardTimeState m_cardTimeState;

    public void StartCardTime(Defines.CardTimeState state)
    {
        m_leftCardTime = m_FixedcardTime[(int)state];
        StartCoroutine(OnCardTimer());
    }

    IEnumerator OnCardTimer()
    {
        while (m_leftCardTime > 0)
        {
            yield return new WaitForSeconds(1f);
            m_leftCardTime--;
            GameManager.GameMgr.TimeEvent.Invoke(m_leftCardTime);
        }

        if(GameManager.GameMgr.CardTimeState != null)
            GameManager.GameMgr.CardTimeState.Invoke(m_cardTimeState);

        if (++m_cardTimeState != Defines.CardTimeState.Count)
        {
            Debug.Log(m_cardTimeState);
            StartCardTime(m_cardTimeState);
        }
        else
        {
            m_cardTimeState = Defines.CardTimeState.SelectingCard;
        }
    }
}
