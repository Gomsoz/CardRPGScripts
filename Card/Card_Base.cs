using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Base : MonoBehaviour
{
    Defines.CardType m_cardState = Defines.CardType.None;
    [SerializeField]
    int m_indexOnSlot = 0;
    public int IndexOnSlot { get { return m_indexOnSlot; } }

    protected Transform m_usingPlayer;
    public int CardMana;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        m_usingPlayer = Managers.Object.Player;
    }

    // 왼쪽 기준 스킬의 범위 -> 마크포인트가 필요하다.
    List<Defines.Position> m_cardRange = new List<Defines.Position>()
    {
        new Defines.Position(-1 ,0),
        new Defines.Position(-2 ,0)
    };

    public void SetCardInfo(int idx, Defines.CardType type)
    {
        m_indexOnSlot = idx;
        m_cardState = type;
    }

    public virtual void Used()
    {
        Debug.Log($"Skill Used ({transform.name}) in {m_cardState}({m_indexOnSlot})");
        if (m_cardRange != null)
            GameManager.MarkPoint.InstantiateMarkPoint(this, m_cardRange);
    }

    public virtual void UsedWithMarkPoint(Defines.Position pos)
    {
    }
}
