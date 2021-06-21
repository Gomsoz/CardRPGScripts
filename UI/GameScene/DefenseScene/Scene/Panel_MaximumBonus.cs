using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum BonusType
{
    Red,
    Blue,
    Green,
    Count,
}

public class Panel_MaximumBonus : UI_Base
{
    CreateMaximumBonus m_createMaximumBonus;


    /* 슬롯의 색을 결정해준다.
     * RED - 공격적인 보너스 (공격력 증가)
     * BLUE - 방어적인 보너스 (체력 증가)
     * GREEN - 이외의 보너스 (억제기의 체력회복)
     */

    [SerializeField]
    Transform m_leftBonusSlot;
    [SerializeField]
    Transform m_rightBonusSlot;

    // 보너스 타입에 따라 색깔을 변경한다.
    Color[] m_boardColors = new Color[(int)BonusType.Count]
    {
        new Color(255, 180, 172), // red
        new Color(77, 144, 224), // blue
        new Color(160, 230, 165) // green
    };

    string m_headlineSpritesPath = $"Sprites/Headline";
    Sprite[] m_headlineSprites = new Sprite[(int)BonusType.Count];

    public override void Init()
    {
        // 제목부분의 스프라이트를 로드하여 저장한다.
        for(int i = 0; i < (int)BonusType.Count; i++)
        {
            string _targetColor = System.Enum.GetName(typeof(BonusType), (BonusType)i);
            m_headlineSprites[i] = Managers.Resources.Load<Sprite>($"{m_headlineSpritesPath}/Headline_{_targetColor}");
        }

        m_createMaximumBonus = new CreateMaximumBonus();
    }

    private void OnEnable()
    {
        if (m_createMaximumBonus == null)
        {
            Init();
            return;
        }
        SetSlotAtRandomType();
    }

    void SetSlotAtRandomType()
    {
        int _bonusType = UnityEngine.Random.Range(0, (int)BonusType.Count);

        // 왼쪽 슬롯 등록
        m_leftBonusSlot.GetComponent<Slot_MaximumBonus>().SetMaximumBonusSlot(
            m_createMaximumBonus.GetRadnomeBonusforType((BonusType)_bonusType), 
            m_boardColors[_bonusType], 
            m_headlineSprites[_bonusType]);

        _bonusType = UnityEngine.Random.Range(0, (int)BonusType.Count);

        // 오른쪽 슬롯 등록
        m_rightBonusSlot.GetComponent<Slot_MaximumBonus>().SetMaximumBonusSlot(
            m_createMaximumBonus.GetRadnomeBonusforType((BonusType)_bonusType),
            m_boardColors[_bonusType],
            m_headlineSprites[_bonusType]);
    }
}
