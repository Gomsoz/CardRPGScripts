using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest_Hunt : Quest_Base
{ 
    string m_huntingTarget;
    int m_curHuntingCnt;
    int m_goalHuntingCnt;

    // 레벨이 올라가서 잠금이 해제된다던지 하면 List로 작성하여 범위를 늘리거나 줄임.
    string[] m_randomHuntingTarget = new string[2]
        { "Pig", "PirateBoar" };
    int m_randomHuntingCnt = 3;

    public Quest_Hunt() { }

    public Quest_Hunt(string target, int cnt, QuestType type)
    {
        m_huntingTarget = target;
        m_goalHuntingCnt = cnt;
        m_questType = type;

        Init();
    }

    public override void Clear()
    {
        Managers.Object.DyingEnemyEvent -= DyingEnemyEvtListner;
    }

    public override void Random(QuestType type)
    {
        m_questType = type;
        m_huntingTarget = m_randomHuntingTarget[UnityEngine.Random.Range(0, m_randomHuntingTarget.Length)];
        m_goalHuntingCnt = UnityEngine.Random.Range(1, m_randomHuntingCnt);

        Init();
        Managers.Quest.AddQuest(type, this);
    }

    public override void Init()
    {
        m_context = $"{m_huntingTarget} 을/를 {m_goalHuntingCnt}마리 잡으세요 !";
        Managers.Object.DyingEnemyEvent += DyingEnemyEvtListner;

        // 퀘스트 코드를 이용하던지 하여 추후에 json 파일로 저장된 데이터를 불러옴.
        m_questReward = new Quest_Reward(100, 50);
        Managers.UI.GetSceneUI<UI_QuestScene>().ChangeQuestProgressText(m_questType, $"{m_huntingTarget} 사냥 퀘스트 : {m_curHuntingCnt}/{m_goalHuntingCnt}");
    }


    public void DyingEnemyEvtListner(Char_EnemyStats stat)
    {
        if(stat.Name == m_huntingTarget)
        {
            m_curHuntingCnt++;
            Managers.UI.GetSceneUI<UI_QuestScene>().ChangeQuestProgressText(m_questType, $"{m_huntingTarget} 사냥 퀘스트 : {m_curHuntingCnt}/{m_goalHuntingCnt}");
            Debug.Log($"{m_huntingTarget} 사냥 퀘스트 : {m_curHuntingCnt}/{m_goalHuntingCnt}");
        }

        if(m_curHuntingCnt >= m_goalHuntingCnt)
        {
            Clear();
            Managers.Quest.FinishQuset(m_questType);
        }
    }
}

public class Quest_MoveToGoal : Quest_Base
{
    Defines.Position m_goalPos;
    string m_markPath = "Prefabs/Object/Mark/GoalMark";

    public Quest_MoveToGoal() { }

    public Quest_MoveToGoal(QuestType type, Defines.Position pos)
    {
        m_questType = type;
        m_goalPos = pos;

        Init();
    }

    public override void Clear()
    {
        
    }

    public override void Random(QuestType type)
    {
        m_questType = type;
        m_goalPos = new Defines.Position(UnityEngine.Random.Range(0, Managers.Board.BoardWidth), UnityEngine.Random.Range(0, Managers.Board.BoardHeight));
        
        Init();
        Managers.Quest.AddQuest(type, this);
    }

    public override void Init()
    {
        m_context = $"출구를 찾으세요!";
        GameObject mark = Managers.Resources.Instantiate(m_markPath);
        mark.GetComponent<GoalMarkBehavior>().CollisionEvt += ChkCollision;
        mark.transform.position = Managers.Board.BoardPosToWorldPos(m_goalPos);

        // 퀘스트 코드를 이용하던지 하여 추후에 json 파일로 저장된 데이터를 불러옴.
        m_questReward = new Quest_Reward(300, 130);

    }

    public void ChkCollision()
    {
        Clear();
        Managers.Quest.FinishQuset(m_questType);
    }
}
