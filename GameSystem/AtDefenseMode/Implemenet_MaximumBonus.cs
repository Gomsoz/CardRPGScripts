using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMaximumBonus
{
    Dictionary<BonusType, List<MaximumBonus_Base>> m_bonusList = new Dictionary<BonusType, List<MaximumBonus_Base>>();

    int m_bonusLevel;

    public CreateMaximumBonus()
    {
        for(int i = 0; i < (int)BonusType.Count; i++)
        {
            m_bonusList.Add((BonusType)i, new List<MaximumBonus_Base>());
        }

        AddBonus();
    }

    /*
     * 보너스 레벨에 따라서 보너스의 종류를 변경시킨다. -> 데이터화
     * 현재는 하나씩 밖에 존재하지 않기 때문에 추후에 추가해야함.
     */

    public void AddBonus()
    {
        m_bonusList[BonusType.Red].Add(new PlayerAttackDamageBonus());
        m_bonusList[BonusType.Blue].Add(new PlayerArmorBonus());
        m_bonusList[BonusType.Green].Add(new RecoverySuppressorHP());
    }

    public MaximumBonus_Base GetRadnomeBonusforType(BonusType type)
    {
        int _targetBonusIdx = UnityEngine.Random.Range(0, m_bonusList[type].Count);
        return m_bonusList[type][_targetBonusIdx];
    }
}

public abstract class MaximumBonus_Base
{
    protected BonusType m_bonusType;
    protected string m_bonusName;
    public string BonusName { get { return m_bonusName; } }

    protected string m_explainText;
    public string ExplainText { get { return m_explainText; } }

    protected int m_bonusStack;


    protected virtual void Init()
    {
        m_bonusStack++;
    }

    public abstract void ApplyMaximumBonus();

}

public class RedMaximumBonus : MaximumBonus_Base
{
    protected override void Init()
    {
        m_bonusType = BonusType.Red;
    }

    public override void ApplyMaximumBonus() { }
}

public class BlueMaximumBonus : MaximumBonus_Base
{
    protected override void Init()
    {
        m_bonusType = BonusType.Blue;
    }

    public override void ApplyMaximumBonus() { }
}

public class GreenMaximumBonus : MaximumBonus_Base
{
    protected override void Init()
    {
        m_bonusType = BonusType.Green;
    }

    public override void ApplyMaximumBonus() { }
}

#region Implenmenet
/*
 * 이름과 설명을 가지고있음 -> 나중에 데이터로 보관 필요
 */
public class PlayerAttackDamageBonus : RedMaximumBonus
{
    
    public PlayerAttackDamageBonus()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        m_bonusName = "공격력 증가";
        m_explainText = "플레이어의 공격력을 증가시킨다.";
    }

    public override void ApplyMaximumBonus()
    {
        Debug.Log("플레이어의 공격력이 증가되었습니다.");
    }
}

public class PlayerArmorBonus : BlueMaximumBonus
{
    public PlayerArmorBonus()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        m_bonusName = "방어력 증가";
        m_explainText = "플레이어의 방어력을 증가시킨다.";
    }

    public override void ApplyMaximumBonus()
    {
        Debug.Log("플레이어의 방어력이 증가되었습니다.");
    }
}

public class RecoverySuppressorHP : GreenMaximumBonus
{
    public RecoverySuppressorHP()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();
        m_bonusName = "억제기 재생성";
        m_explainText = "억제기의 체력을 모두 회복시킨다.";
    }

    public override void ApplyMaximumBonus()
    {
        Debug.Log("억제기의 체력이 모두 회복되었습니다.");
    }
}
#endregion