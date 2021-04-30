using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager
{
    string[] m_defaultObjectPath = new string[(int)Defines.ObjectType.Count]
    {
        "Prefabs/Object/Player",
        "Prefabs/Object/Enemy",
        "Prefabs/Object/Item",
    };

    Transform m_player;
    public Transform Player { get { return m_player; } }

    Transform m_enemyHolder;

    public Action<Char_EnemyStats> DyingEnemyEvent = null;

    public void Init()
    {
        m_enemyHolder = new GameObject { name = "EnemyHolder" }.transform;
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
}
