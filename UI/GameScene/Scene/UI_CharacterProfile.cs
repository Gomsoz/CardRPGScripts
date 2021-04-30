using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_CharacterProfile : UI_Scene
{
    enum Texts
    {
        Level,
        HP,
        MP,
        Attack,
        Armor,
    }

    enum btns
    {
        Btn_HP,
        Btn_MP,
        Btn_Attack,
        Btn_Armor
    }

    int m_levelupPoint;
    Transform LevelupButtons;

    public override void Init()
    {
        base.Init();

        LevelupButtons = transform.Find("LevelupButtons");

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(btns));

        AddUIHandler(Get<Button>((int)btns.Btn_HP).gameObject, ClickHPUpBtn);
        AddUIHandler(Get<Button>((int)btns.Btn_MP).gameObject, ClickMPUpBtn);
        AddUIHandler(Get<Button>((int)btns.Btn_Attack).gameObject, ClickAttackUpBtn);
        AddUIHandler(Get<Button>((int)btns.Btn_Armor).gameObject, ClickArmorUpBtn);

        LevelupButtons.gameObject.SetActive(false);
    }

    public void IncreaseLevelupPoint()
    {
        if (m_levelupPoint == 0)
            LevelupButtons.gameObject.SetActive(true);
        m_levelupPoint++;
    }

    public void UpdateText_Stats(Char_PlayerStats playerStats)
    {
        Get<Text>((int)Texts.Level).text = $"Level : {playerStats.Level.ToString()}";
        Get<Text>((int)Texts.HP).text = $"HP : {playerStats.HP.ToString()}";
        Get<Text>((int)Texts.MP).text = $"MP : {playerStats.MP.ToString()}";
        Get<Text>((int)Texts.Attack).text = $"Attack : {playerStats.AttackDamage.ToString()}";
        Get<Text>((int)Texts.Armor).text = $"Armor : {playerStats.Armor.ToString()}";
    }

    public void ClickHPUpBtn(PointerEventData evt)
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyDefaultCharacterStats(StatType.MaxHP, 50);
        DecreaseLevelupPoint();
    }
    
    public void ClickMPUpBtn(PointerEventData evt)
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyDefaultCharacterStats(StatType.MaxMP, 50);
        DecreaseLevelupPoint();
    }

    public void ClickAttackUpBtn(PointerEventData evt)
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyDefaultCharacterStats(StatType.Attack, 3);
        DecreaseLevelupPoint();
    }

    public void ClickArmorUpBtn(PointerEventData evt)
    {
        Managers.Object.Player.GetComponent<Char_PlayerCtr>().ModifyDefaultCharacterStats(StatType.Armor, 1);
        DecreaseLevelupPoint();
    }

    void DecreaseLevelupPoint()
    {
        m_levelupPoint--;
        if(m_levelupPoint == 0)
            LevelupButtons.gameObject.SetActive(false);

    }
}
