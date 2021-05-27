using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
    Dictionary<string, Board_Base> m_boards = new Dictionary<string, Board_Base>();
    Board_Base m_curBoard;

    SetupMapTiles c_setupMapTils = new SetupMapTiles();

    int m_boardWidth;
    public int BoardWidth { get { return m_boardWidth; } }
    int m_boardHeight;
    public int BoardHeight { get { return m_boardHeight; } }

    // 생성한 보드의 배열
    Transform[,] m_tilesOnBoard;
    // 보드 위의 오브젝트의 배열
    Transform[,] m_objectOnBoard;

    public void AwakeInit()
    {
        c_setupMapTils.AwakeInit();
    }

    public void LoadBoard(string mapCode)
    {
        // 저장된 보드 데이터 확인
        if (m_boards.TryGetValue(mapCode, out m_curBoard) == false)
        {
            // 저장된 보드가 없으므로 보드 데이터를 불러옴.
            if (Managers.Json.LoadBoardData(mapCode, out m_curBoard) == false)
            {
                Debug.LogError($"Faild to Load Board : {mapCode}");
                return;
            }
            m_boards.Add(mapCode, m_curBoard);
        }

        SetBoard();
    }

    void SetBoard()
    {
        m_boardWidth = m_curBoard.BoardWidth;
        m_boardHeight = m_curBoard.BoardHeight;

        m_tilesOnBoard = new Transform[m_boardWidth, m_boardHeight];
        m_objectOnBoard = new Transform[m_boardWidth, m_boardHeight];

        m_tilesOnBoard =  c_setupMapTils.ConstructTiles(m_curBoard);
    }

    public bool ChkAndAddObjOnBoard(Defines.Position pos, Transform obj, bool overlap = false)
    {
        // 보드의 경계 검사, 해당 좌표에 오브젝트가 있는지 확인
        if (ChkBoundary(pos) == false || ChkObjOnBoard(pos) == false)
            return false;

        obj.position = m_tilesOnBoard[(int)pos.posX, (int)pos.posY].position + new Vector3(0, 1, 0);
        m_objectOnBoard[(int)pos.posX, (int)pos.posY] = obj;
        return true;
    }

    public bool ChkAndRemoveObjOnBoard(Defines.Position pos)
    {
        // 보드의 경계 검사
        if (ChkBoundary(pos) == false)
            return false;

        m_objectOnBoard[(int)pos.posX, (int)pos.posY] = null;
        return true;
    }

    public Vector2 BoardPosToWorldPos(Defines.Position pos)
    {
        return m_tilesOnBoard[(int)pos.posX, (int)pos.posY].position;
    }

    public bool ChkMoveObjOnBoard(Transform charater, Defines.Position pos)
    {
        if (charater == null || ChkBoundary(pos) == false)
            return false;

        if(m_objectOnBoard[(int)pos.posX, (int)pos.posY] != null)
        {
            Debug.Log("움직일 수 없습니다.");
            return false;
        }

        Defines.Position curPos = charater.GetComponent<Char_BaseCtr>().Position;

        m_objectOnBoard[(int)pos.posX, (int)pos.posY] = charater;
        m_objectOnBoard[(int)curPos.posX, (int)curPos.posY] = null;

        return true;
    }

    public Transform GetObjOnBoard(Defines.Position pos)
    {
        if (ChkBoundary(pos) == false)
            return null;

        return m_objectOnBoard[(int)pos.posX, (int)pos.posY];
    }

    public bool ChkObjOnBoard(Defines.Position pos)
    {
        if (m_objectOnBoard[(int)pos.posX, (int)pos.posY] != null)
        {
            Debug.Log($"The Object is existed ({(int)pos.posX}, {(int)pos.posY})");
            return false;
        }

        return true;
    }

    public bool ChkBoundary(Defines.Position pos)
    {
        if (pos.posX < 0 || pos.posX >= m_boardWidth || pos.posY < 0 || pos.posY >= m_boardHeight)
        {
            Debug.Log($"The Object is out of bounds ({(int)pos.posX}, {(int)pos.posY})");
            return false;
        }
        return true;
    }
}
