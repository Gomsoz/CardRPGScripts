using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    HP,
    MaxHP,
    MaxMP,
    MP,
    Attack,
    Armor,
}

public abstract class Char_BaseCtr : MonoBehaviour, InterfaceClass.IPosition
{
    protected Transform m_anchorDamage;
    protected GameObject m_DamageText;

    [SerializeField]
    Defines.Position m_position;
    public Defines.Position Position
    {
        get
        {
            return m_position;
        }
        set
        {
            m_position = value;
        }
    }

    protected Animator m_animator;

    int m_speed = 3;

    private void Start()
    {
        m_anchorDamage = transform.Find("AnchorDamage");
        m_DamageText = Managers.UI.GetSceneUI<UI_Status>().InstantiateDamageUI();
        m_DamageText.transform.position = Camera.main.WorldToScreenPoint(m_anchorDamage.transform.position);
    }

    public void SetPosition(Defines.Position pos)
    {
        m_position = pos;
        transform.position = Managers.Board.BoardPosToWorldPos(m_position) + new Vector2(0, 1);
    }

    // pos 를 향해 움직임.
    public virtual void CharacterMove(Defines.Position pos)
    {
        if (Managers.Board.ChkMoveObjOnBoard(transform, pos) == false)
            return;

        StartCoroutine(SmoothMove(pos));
    }

    IEnumerator SmoothMove(Defines.Position pos)
    {
        Vector3 targetPos = Managers.Board.BoardPosToWorldPos(pos) + new Vector2(0, 1);
        Vector3 dir = targetPos - transform.position;

        if(dir.x > 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);

        while (dir.magnitude > 0.1f)
        {
            m_DamageText.transform.position = Camera.main.WorldToScreenPoint(m_anchorDamage.transform.position);
            dir = targetPos - transform.position;
            yield return null;
            float moveDist = Mathf.Clamp(m_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;

            if(transform.tag == "Player")
                Managers.Camera.UpdateCameraPos(Defines.CameraType.Main);
        }

        SetPosition(pos);
        m_animator.SetInteger("State", (int)Defines.CharacterState.Idle);
    }

    public abstract void AttackOtherCharacter(Transform other);

    // 캐릭터에 데미지를 가함.
    public abstract void UnderAttackedCharacter(float damage, Transform attacker);
}
