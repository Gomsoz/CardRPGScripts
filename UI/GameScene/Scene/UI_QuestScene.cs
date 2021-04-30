using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_QuestScene : UI_Scene
{
    enum ContentsTexts
    {
        Text_MainContents,
        Text_MainProgress,
        Text_MainReward,
        Text_SubContents,
        Text_SubProgress,
        Text_SubReward,
    }

    enum Btns
    {
        Btn_NewMainQuest,
        Btn_NewSubQuest,
    }

    const int m_extraPointAtProgress = 1;
    const int m_extraPointAtReward = 2;
    const int m_gapBetweenQuests = 3;

    string m_defaultText = "퀘스트가 없습니다.";

    public override void Init()
    {
        base.Init();

        Bind<Text>(typeof(ContentsTexts));
        Bind<Button>(typeof(Btns));

        AddUIHandler(Get<Button>((int)Btns.Btn_NewMainQuest).gameObject, ClickNewMainQuestButton);
        AddUIHandler(Get<Button>((int)Btns.Btn_NewSubQuest).gameObject, ClickNewSubQuestButton);

        //Get<Button>((int)Btns.Btn_NewMainQuest).gameObject.SetActive(false);
        //Get<Button>((int)Btns.Btn_NewSubQuest).gameObject.SetActive(false);
    }

    public void ChangeAllQuestText(QuestType type, Quest_Base quest)
    {
        int startIdx = (int)type * m_gapBetweenQuests;

        Get<Text>(startIdx).text = quest.Context;
        Get<Text>(startIdx + m_extraPointAtProgress).text = quest.ProgressText;
        Get<Text>(startIdx + m_extraPointAtReward).text = quest.RewardText;
    }

    public void ChangeDefaultQuestText(QuestType type)
    {
        int startIdx = (int)type * m_gapBetweenQuests;

        Get<Text>(startIdx).text = m_defaultText;
        Get<Text>(startIdx + m_extraPointAtProgress).text = "";
        Get<Text>(startIdx + m_extraPointAtReward).text = "";

        Get<Button>((int)type).gameObject.SetActive(true);
    }

    public void ChangeQuestProgressText(QuestType type, string text)
    {
        int startIdx = (int)type * m_gapBetweenQuests;

        Get<Text>(startIdx + m_extraPointAtProgress).text = text;
    }

    public void ClickNewMainQuestButton(PointerEventData evt)
    {
        Managers.Quest.AddRandomQuest(QuestType.MainQuest);
        Get<Button>((int)Btns.Btn_NewMainQuest).gameObject.SetActive(false);
    }

    public void ClickNewSubQuestButton(PointerEventData evt)
    {
        Managers.Quest.AddRandomQuest(QuestType.SubQuest);
        Get<Button>((int)Btns.Btn_NewSubQuest).gameObject.SetActive(false);
    }
}
