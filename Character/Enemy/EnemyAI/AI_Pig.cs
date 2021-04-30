using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Pig : AI_BaseEnemy
{
    protected override void Init()
    {
        m_defaultBehaviorTurn = 2;
        base.Init();
    }

    public override void Behavior()
    {
        int dis =  (int)ChkDistanceFromPlayer();

        // 거리가 1이 넘으면 이동명령
        if(dis > 1)
        {
            Defines.Direction dir = MovingDirection(ChkPositionFromPlayer());
            Debug.Log(dir);
            m_controller.CharacterMove(m_controller.Position.PositionForDir(dir));
            return;
        }

        m_controller.AttackOtherCharacter(Managers.Object.Player);
    }

}
