using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest_Base
{
    protected Quest_Reward m_questReward;

    protected QuestType m_questType;
    protected string m_context;
    protected string m_progressText = "";
    public string Context { get { return m_context; } }
    public string RewardText { get { return m_questReward.RewardText; } }
    public string ProgressText { get { return m_progressText; } }

    // 퀘스트 등록 시 호출
    public abstract void Init();

    // 퀘스트 종료 시 호출
    public abstract void Clear();

    // 퀘스트 랜덤 생성 시 호출
    public abstract void Random(QuestType type);
    public Quest_Reward GetReward()
    {
        return m_questReward;
    }
}
