using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_PirateBoar : AI_BaseEnemy
{
    GameObject m_cannonBall;

    protected override void Init()
    {
        m_defaultBehaviorTurn = 1;
        m_cannonBall = Managers.Resources.Load<GameObject>($"Prefabs/Object/Enemy/Weapon/PirateBoarCannonBall");
        base.Init();
    }

    public override void Behavior()
    {
        int dis = (int)ChkDistanceFromPlayer();

        // 거리가 2가 넘으면 이동명령
        if (dis > 2)
        {
            Defines.Direction dir = MovingDirection(ChkPositionFromPlayer());
            Debug.Log(dir);
            m_controller.CharacterMove(m_controller.Position.PositionForDir(dir));
            return;
        }

        GameObject.Instantiate(m_cannonBall);
    }
}
