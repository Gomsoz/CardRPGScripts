using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.FantasyMonsters.Scripts;

public class Char_EnemyCtr : Char_BaseCtr
{
    public Char_EnemyStats m_enemyStats;

    protected override void Init()
    {
        m_enemyStats = new Char_EnemyStats
        {
            HP = 10,
            AttackDamage = 10,
            Name = transform.name.Split('(')[0],
            Drop_Coin = 30,
            Drop_Exp = 16,
        };

        m_animator = gameObject.GetComponent<Animator>();

        base.Init();
    }

    public override void AttackOtherCharacter(Transform other)
    {
        Char_BaseCtr otherChar = other.GetComponent<Char_BaseCtr>();
        otherChar.UnderAttackedCharacter(m_enemyStats.AttackDamage, transform);
    }

    public override void UnderAttackedCharacter(float damage, Transform attacker)
    {
        m_enemyStats.HP += (m_enemyStats.Armor - damage);
        Debug.Log("HP : " + m_enemyStats.HP + "Hit : " + (m_enemyStats.Armor - damage));

        if (m_enemyStats.HP < 1)
        {
            Managers.Board.ChkAndRemoveObjOnBoard(Position);
            m_animator.SetInteger("State", (int)MonsterState.Death);

            attacker.GetComponent<Char_PlayerCtr>().AddExp(m_enemyStats.Drop_Exp);
            attacker.GetComponent<Char_PlayerCtr>().AddCoin(m_enemyStats.Drop_Coin);

            Managers.Object.DyingEnemyEvent(m_enemyStats);

            GameManager.CreateItembox.InstantiateItemBox(transform.position, m_enemyStats);

            GetComponent<AI_BaseEnemy>().ClearTimeTracking();

            Destroy(m_DamageText.gameObject);
            Destroy(this.gameObject,1);
        }
    }

    public void EnemyMoveToPlayer()
    {

    }
}
