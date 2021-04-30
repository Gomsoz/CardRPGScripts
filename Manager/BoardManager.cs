﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager
{
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
        SetBoard(10, 10);
    }

    public void SetBoard(int width, int height)
    {
        m_boardWidth = width;
        m_boardHeight = height;

        m_tilesOnBoard = new Transform[m_boardWidth, m_boardHeight];
        m_objectOnBoard = new Transform[m_boardWidth, m_boardHeight];


        c_setupMapTils.ConstructTiles(m_boardWidth, m_boardHeight, out m_tilesOnBoard);
    }

    public bool ChkAndAddObjOnBoard(Defines.Position pos, Transform obj, bool overlap = false)
    {
        // 해당 좌표에 오브젝트가 있는지 확인
        if (m_objectOnBoard[(int)pos.posX, (int)pos.posY] != null)
        {
            Debug.Log($"The Object is existed ({(int)pos.posX}, {(int)pos.posY})");
            return false;
        }

        // 보드의 경계 검사
        if (ChkBoundary(pos) == false)
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
        if (charater == null)
            return false;

        if (ChkBoundary(pos) == false)
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