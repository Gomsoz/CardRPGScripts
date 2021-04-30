using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_BaseEnemy : MonoBehaviour
{
    protected int m_defaultBehaviorTurn;
    protected int m_nextBehaviorTurn;

    protected Defines.Position m_playerPos;
    protected Defines.Position m_enemyPos;

    protected Char_BaseCtr m_controller;

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        m_controller = GetComponent<Char_BaseCtr>();
        m_nextBehaviorTurn = m_defaultBehaviorTurn;
        GameManager.GameMgr.CardTimeState += ChkBehaviorTurn;
    }

    public void ClearTimeTracking()
    {
        GameManager.GameMgr.CardTimeState -= ChkBehaviorTurn;
    }

    public void ChkBehaviorTurn(Defines.CardTimeState state)
    {
        if (state == Defines.CardTimeState.ThirdCard)
            return;

        // 남은 턴 수가 '0' 이면 행동
        if (m_nextBehaviorTurn != 0)
        {
            m_nextBehaviorTurn--;
            return;
        }

        Vector3 dir = Managers.Object.Player.position - transform.position;
        m_controller.RotateCharacter(dir);

        Behavior();
        m_nextBehaviorTurn = m_defaultBehaviorTurn;
    }

    public abstract void Behavior();

    protected float ChkDistanceFromPlayer()
    {
        m_playerPos = Managers.Object.Player.GetComponent<Char_BaseCtr>().Position;
        m_enemyPos = m_controller.Position;

        return Mathf.Abs(m_playerPos.posX - m_enemyPos.posX) + Mathf.Abs(m_playerPos.posY - m_enemyPos.posY);
    }

    protected Defines.Position ChkPositionFromPlayer()
    {
        m_playerPos = Managers.Object.Player.GetComponent<Char_BaseCtr>().Position;
        m_enemyPos = m_controller.Position;

        return m_playerPos - m_enemyPos;
    }

    protected Defines.Direction MovingDirection(Defines.Position gapPos)
    {
        if (Mathf.Abs(gapPos.posX) >= Mathf.Abs(gapPos.posY))
            return gapPos.posX < 0 ? Defines.Direction.Left : Defines.Direction.Right;
        else
            return gapPos.posY < 0 ? Defines.Direction.Down : Defines.Direction.Up;
    }
}
