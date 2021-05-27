using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Time_CardSystem : MonoBehaviour
{
    int m_leftCardTime = 0;

    Coroutine m_timeCoroutine = null;

    int[] m_FixedcardTime = new int[(int)Defines.CardTimeState.Count]
    {
        10,5,5,5
    };
    Defines.CardTimeState m_cardTimeState;

    private void Start()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (m_timeCoroutine != null)
            StopCoroutine(m_timeCoroutine);
    }

    public void StartCardTime(Defines.CardTimeState state)
    {
        m_leftCardTime = m_FixedcardTime[(int)state];
        m_timeCoroutine = StartCoroutine(OnCardTimer());
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
