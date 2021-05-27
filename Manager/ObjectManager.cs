using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum EnemyType
{
    Pig,
}

public class ObjectManager
{
    string[] m_defaultObjectPath = new string[(int)Defines.ObjectType.Count]
    {
        "Prefabs/Object/Player",
        "Prefabs/Object/Enemy",
        "Prefabs/Object/Item",
        "Prefabs/Object/Mark",
    };

    Factory_Enemy m_enemyFactory;

    Transform m_player;
    public Transform Player { get { return m_player; } }

    Transform m_enemyHolder;

    public Action<Char_EnemyStats> DyingEnemyEvent = null;

    public void Init()
    {
        m_enemyHolder = new GameObject { name = "EnemyHolder" }.transform;
    }

    public Transform SpawnEnemy(string enemyName, Defines.Position pos)
    {
        if (Managers.Board.ChkObjOnBoard(pos) == false)
        {
            Debug.Log($"Faild to Create Enemy : {enemyName}");
            return null;
        }

        m_enemyFactory = new Factory_Pig();

        GameObject go = m_enemyFactory.CreateEnemy(enemyName, pos);
        if (go == null)
            return null;

        Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Enemy, go, pos);
        return go.transform;
    }

    public Transform SpawnObjectOnBoard(Defines.ObjectType type, string objectName, Defines.Position pos)
    {
        string objectPath = $"{m_defaultObjectPath[(int)type]}/{objectName}";

        GameObject go = Managers.Resources.Instantiate(objectPath);
        go.GetComponent<Char_BaseCtr>().SetPosition(pos);

        if (type == Defines.ObjectType.Enemy)
            go.transform.parent = m_enemyHolder;
        else if (type == Defines.ObjectType.Player)
            m_player = go.transform;

        Managers.Board.ChkAndAddObjOnBoard(pos, go.transform);
        return go.transform;
    }

    public Transform SpawnObjectOnBoard(Defines.ObjectType type, GameObject go, Defines.Position pos)
    {
        go.GetComponent<Char_BaseCtr>().SetPosition(pos);

        if (type == Defines.ObjectType.Enemy)
            go.transform.parent = m_enemyHolder;
        else if (type == Defines.ObjectType.Player)
            m_player = go.transform;

        Managers.Board.ChkAndAddObjOnBoard(pos, go.transform);
        return go.transform;
    }
}
