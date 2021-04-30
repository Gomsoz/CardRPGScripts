using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    MainQuest,
    SubQuest,
    Count,
}

public enum ImplementedQuests
{
    Quest_MoveToGoal,
    Quest_Hunt,
    Count,
}

public class QuestManager
{

    UI_QuestScene m_questScene;

    Dictionary<QuestType, Quest_Base> m_questList = new Dictionary<QuestType, Quest_Base>();


    public void InstantiateQuestUI()
    {
        if(m_questScene != null)
        {
            Debug.Log("이미 퀘스트 창이 생성되어 있습니다.");
            return;
        }

        m_questScene = Managers.UI.ShowSceneUI<UI_QuestScene>(Defines.SceneType.GameScene);

        m_questScene.gameObject.SetActive(false);
    }

    public void AddQuest(QuestType type, Quest_Base quest)
    {
        if (m_questScene == null)
            return;

        if (m_questList.ContainsKey(type))
        {
            Debug.Log($"퀘스트가 이미 있습니다.");
            return;
        }

        m_questList.Add(type, quest);
        m_questScene.ChangeAllQuestText(type, quest);      
    }

    public void AddRandomQuest(QuestType type)
    {
        int targetIdx = UnityEngine.Random.Range(0, (int)ImplementedQuests.Count);

        Quest_Base randomQuest = null;

        switch (targetIdx)
        {
            case (int)ImplementedQuests.Quest_Hunt:
                randomQuest = new Quest_Hunt();
                break;
            case (int)ImplementedQuests.Quest_MoveToGoal:
                randomQuest = new Quest_MoveToGoal();
                break;
            default:
                break;
        }

        if (randomQuest == null)
            return;

        randomQuest.Random(type);
    }

    public void FinishQuset(QuestType type)
    {
        Debug.Log($"퀘스트 성공!");
        m_questScene.ChangeDefaultQuestText(type);
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().AddCoin(m_questList[type].GetReward().RewardCoin);
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().AddExp(m_questList[type].GetReward().RewardExp);
        m_questList.Remove(type);
    }
}
