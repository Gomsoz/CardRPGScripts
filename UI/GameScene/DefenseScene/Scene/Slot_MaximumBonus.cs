using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot_MaximumBonus : UI_Base
{
    MaximumBonus_Base m_curBonus;

    enum Texts
    {
        Text_Explain,
        Text_Headline,
    }

    enum Images
    {
        Place_ExplainBoard,
        Place_BonusName,
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        //Debug.Log( Get<Button>((int)Buttons.Place_ExplainBoard).gameObject.name);
        AddUIHandler(Get<Image>((int)Images.Place_ExplainBoard).gameObject, Selectbonus);
    }

    public void SetMaximumBonusSlot(MaximumBonus_Base kindOfBonus, Color boardColor, Sprite headlineColor)
    {
        m_curBonus = kindOfBonus;
        ChangeBonusSlotColors(boardColor, headlineColor);
        ChangeBonusSlotText(kindOfBonus.BonusName, kindOfBonus.ExplainText);
    }

    public void ChangeBonusSlotColors(Color boardColor, Sprite headlineColor)
    {
        Get<Image>((int)Images.Place_ExplainBoard).color = boardColor;
        Get<Image>((int)Images.Place_BonusName).sprite = headlineColor;   
    }

    public void ChangeBonusSlotText(string headline, string explain)
    {
        Get<Text>((int)Texts.Text_Headline).text = headline;
        Get<Text>((int)Texts.Text_Explain).text = explain;

        Debug.Log($"Change Texts {headline} : {explain}");
    }

    public void Selectbonus(PointerEventData evt)
    {
        Debug.Log("Select");
        m_curBonus.ApplyMaximumBonus();
        Managers.UI.GetSceneUI<UI_MaximumBonus>().gameObject.SetActive(false);
        GameManager.GameMgr.GamePause();
    }
}
