using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slider_SuppressorGauge : UI_Base
{
    int m_value = 0;
    int m_maxValue;
    Slider m_sliderComponent;

    GameObject m_fillArea;
    bool m_isIncreaseGauge = true;

    enum GameObjects
    {
        FillArea,
    }

    private void Awake()
    {
        m_fillArea = transform.Find("Fill Area").gameObject;
        m_fillArea.SetActive(false);

        m_sliderComponent = GetComponent<Slider>();
        m_maxValue = (int)m_sliderComponent.maxValue;

        StartCoroutine(StartIncreaseGauge());
    }

    public override void Init()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator StartIncreaseGauge()
    {
        while (m_isIncreaseGauge)
        {
            yield return new WaitForSeconds(2f);
            ChkAndAddValue(49);
        }
    }

    public void ChkAndAddValue(int value)
    {
        // 값을 더함
        m_value += value;
        m_fillArea.SetActive(true);

        // 최대값을 넘어가면 값을 초기화시킴
        if (m_value >= m_maxValue)
        {
            m_fillArea.SetActive(false);
            m_value = 0;
            Scene_Defense.Instance.SuppressorGaugeReachesTheMaximum.Invoke();
        }

        
        m_sliderComponent.value = m_value;
    }

}
