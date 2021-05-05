using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;

public class Char_PlayerCtr : Char_BaseCtr
{
    Char_PlayerStats m_playerStats;
    public Char_PlayerStats PlayerStats { get { return m_playerStats; } }

    Char_CommonStats m_defaultStats;
    Char_CommonStats m_additionalStats = new Char_CommonStats();
    public Char_CommonStats DefaultStats { get { return m_defaultStats; } }
    public Char_CommonStats AdditionalStats { get { return m_additionalStats; } }

    private void Awake()
    {
        m_playerStats = new Char_PlayerStats()
        {
            HP = 10,
            MaxHP = 10,
            MP = 50,
            MaxMP = 50,
            AttackDamage = 10,
            Level = 1,
            CurExp = 0,
            NextExp = 10,
        };

        m_defaultStats = new Char_CommonStats()
        {
            HP = 10,
            MaxHP = 10,
            MP = 50,
            MaxMP = 50,
            AttackDamage = 10,
        };

        /*for (int i = 0; i < (int)StatType.Count; i++)
        {
            SetCharacterStats((StatType)i);
        }*/

        m_animator = gameObject.GetComponent<Animator>();
        Managers.UI.GetSceneUI<UI_CharacterProfile>().UpdateText_Stats(m_playerStats);
    }

    public void SetPlayerData(Char_PlayerStats playerStats, Char_CommonStats defaultStats, Char_CommonStats additionalStats)
    {
        m_playerStats = playerStats;
        m_defaultStats = defaultStats;
        m_additionalStats = additionalStats;
    }

    public void LoadPlayerData()
    {

    }

    public override void AttackOtherCharacter(Transform other)
    {
        Char_BaseCtr otherChar = other.GetComponent<Char_BaseCtr>();
        otherChar.UnderAttackedCharacter(m_playerStats.AttackDamage, transform);

        m_animator.SetTrigger("Slash1H");
    }

    public override void UnderAttackedCharacter(float damage, Transform attacker = null)
    {
        int totalDamage = (int)m_playerStats.Armor - (int)damage;
        Managers.UI.GetSceneUI<UI_Status>().ChangeDamageText(m_DamageText, totalDamage);

        ModifyDefaultCharacterStats(StatType.HP, totalDamage);
        Debug.Log("HP : " + m_playerStats.HP + "Hit : " + (m_playerStats.Armor - damage));

        m_animator.SetTrigger("Hit");
    }

    public override void CharacterMove(Defines.Position pos)
    {
        m_animator.SetInteger("State", (int)Defines.CharacterState.Walk);
        
        base.CharacterMove(pos);   
    }

    public void AddCoin(int coin)
    {
        m_playerStats.Coin += coin;
        Managers.UI.GetSceneUI<GameSceneUI>().ChangeCoinText(m_playerStats.Coin);
        Debug.Log($"{coin} 획득! 현재 코인 : {m_playerStats.Coin}");
    }

    public void AddExp(int exp)
    {
        m_playerStats.CurExp += exp;
        ChkPlayerLvUp();
        Managers.UI.GetSceneUI<GameSceneUI>().ChangeExpBar((float)m_playerStats.CurExp/m_playerStats.NextExp);
        Debug.Log($"{exp} 획득! 현재 레벨 : {m_playerStats.Level}, 경험치 : {m_playerStats.CurExp}/{m_playerStats.NextExp}");
    }

    public void ChkPlayerLvUp()
    {
        while(m_playerStats.CurExp >= m_playerStats.NextExp)
        {
            m_playerStats.CurExp -= m_playerStats.NextExp;
            m_playerStats.Level++;
            Managers.UI.GetSceneUI<GameSceneUI>().ChangeLevelText(m_playerStats.Level);
            Managers.UI.GetSceneUI<UI_CharacterProfile>().UpdateText_Stats(m_playerStats);
            Managers.UI.GetSceneUI<UI_CharacterProfile>().IncreaseLevelupPoint();
        }        
    }

    public void ModifyDefaultCharacterStats(StatType type, int value)
    {
        switch (type)
        {
            case StatType.HP:
                m_defaultStats.HP += value;
                break;
            case StatType.MaxHP:
                m_defaultStats.MaxHP += value;
                break;
            case StatType.MP:
                m_defaultStats.MP += value;
                break;
            case StatType.MaxMP:
                m_defaultStats.MaxMP += value;
                break;
            case StatType.Attack:
                m_defaultStats.AttackDamage += value;
                break;
            case StatType.Armor:
                m_defaultStats.Armor += value;
                break;
        }
        SetCharacterStats(type);        
    }

    public void ModifyAdditionalCharacterStats(StatType type, int value)
    {
        switch (type)
        {
            case StatType.HP:
                m_additionalStats.HP += value;
                break;
            case StatType.MaxHP:
                m_additionalStats.MaxHP += value;
                break;
            case StatType.MP:
                m_additionalStats.MP += value;
                break;
            case StatType.MaxMP:
                m_additionalStats.MaxMP += value;
                break;
            case StatType.Attack:
                m_additionalStats.AttackDamage += value;
                break;
            case StatType.Armor:
                m_additionalStats.Armor += value;
                break;
        }
        SetCharacterStats(type);
    }

    void SetCharacterStats(StatType type)
    {
        switch (type)
        {
            case StatType.HP:
                m_playerStats.HP = m_defaultStats.HP + m_additionalStats.HP;
                break;
            case StatType.MaxHP:
                m_playerStats.MaxHP = m_defaultStats.MaxHP + m_additionalStats.MaxHP;
                break;
            case StatType.MP:
                m_playerStats.MP = m_defaultStats.MP + m_additionalStats.MP;
                break;
            case StatType.MaxMP:
                m_playerStats.MaxMP = m_defaultStats.MaxMP + m_additionalStats.MaxMP;
                break;
            case StatType.Attack:
                m_playerStats.AttackDamage = m_defaultStats.AttackDamage + m_additionalStats.AttackDamage;
                break;
            case StatType.Armor:
                m_playerStats.Armor = m_defaultStats.Armor + m_additionalStats.Armor;
                break;
        }
        Managers.UI.GetSceneUI<UI_CharacterProfile>().UpdateText_Stats(m_playerStats);
    }
}
