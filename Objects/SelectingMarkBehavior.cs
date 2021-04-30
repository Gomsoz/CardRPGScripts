using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingMarkBehavior : MonoBehaviour, InterfaceClass.IPosition
{
    [SerializeField]
    Defines.Position m_position;
    public Defines.Position Position
    {
        get { return m_position; }
        set { m_position = value; }
    }

    Defines.Position m_playerPos;

    Defines.Position[] m_positions = new Defines.Position[(int)Defines.Direction.Count];

    Defines.Position GenerateDirPosition(Defines.Direction dir)
    {
        Defines.Position returnPos = m_position;
        switch (dir)
        {
            case Defines.Direction.Left:
                break;
            case Defines.Direction.Right:
                returnPos.posX = -m_position.posX;
                returnPos.posY = m_position.posY;
                break;
            case Defines.Direction.Up:
                returnPos.posX = m_position.posY;
                returnPos.posY = -m_position.posX;
                break;
            case Defines.Direction.Down:
                returnPos.posX = m_position.posY;
                returnPos.posY = m_position.posX;
                break;
        }
        return returnPos;
    }

    public void SetPosition(Defines.Direction dir)
    {
        m_position = m_positions[(int)dir];
        if (Managers.Board.ChkBoundary(m_position) == false)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
        Vector2 boardPos = Managers.Board.BoardPosToWorldPos(m_position);
        transform.position = boardPos + new Vector2(0, 1.25f);
    }

    public void SetPosition(Defines.Position pos) 
    {
        m_position = pos;
        m_playerPos = Managers.Object.Player.GetComponent<Char_BaseCtr>().Position;

        for (int i = 0; i < (int)Defines.Direction.Count; i++)
        {
            Defines.Position _pos = GenerateDirPosition((Defines.Direction)i);
            m_positions[i] = new Defines.Position(m_playerPos.posX + _pos.posX, m_playerPos.posY + _pos.posY);
        }
    }



    private void OnMouseDown()
    {
        GameManager.MarkPoint.CallBackMarkPoint(Position);
        GameManager.MarkPoint.RemoveMarkPoint();
    }
}