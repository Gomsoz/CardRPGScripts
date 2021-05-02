using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Base : MonoBehaviour
{
    public int CardMana;

    // 왼쪽 기준 스킬의 범위 -> 마크포인트가 필요하다.
    List<Defines.Position> m_cardRange = new List<Defines.Position>()
    {
        new Defines.Position(-1 ,0),
        new Defines.Position(-2 ,0)
    };

    public virtual void SetCardInfo()
    {

    }

    public virtual void Used()
    {
        if (m_cardRange != null)
            GameManager.MarkPoint.InstantiateMarkPoint(this, m_cardRange);
    }

    public virtual void UsedWithMarkPoint(Defines.Position pos)
    {
    }
}
