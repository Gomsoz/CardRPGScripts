using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_CharacterSlot : UI_Base
{
    bool m_isSaveData = false;
    enum Btns
    {
        SelectBtn,
        CancelBtn,
    }

    enum Texts
    {
        Text_SaveData,
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Btns));
        Bind<Text>(typeof(Texts));

        AddUIHandler(Get<Button>((int)Btns.SelectBtn).gameObject, ClickSelectBtn);
        AddUIHandler(Get<Button>((int)Btns.SelectBtn).gameObject, CancelBtn);

        SetSaveData();
    }

    void SetSaveData()
    {
        int idx = int.Parse(transform.name.Split('_')[2]);
        Char_PlayerStats stats = Managers.Json.LoadPlayerData(idx);

        if (stats == null)
            return;

        Get<Text>((int)Texts.Text_SaveData).text =
            $"Lv : {stats.Level} \n" +
            $"HP : {stats.HP}";

        m_isSaveData = true;
    }

    public void ClickSelectBtn(PointerEventData evt)
    {
        int idx = int.Parse(transform.name.Split('_')[2]);
        GameManager.GameMgr.SaveSlotIdx = idx;
        GameManager.GameMgr.IsLoadData = m_isSaveData;
        Managers.Scene.LoadScene(Defines.SceneType.GameScene);
    }

    public void CancelBtn(PointerEventData evt)
    {

    }
}
