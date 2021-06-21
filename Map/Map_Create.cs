using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType
{
    Common,
    OnlyDefenseMode,
}

public class Map_Create
{
    // map
    Dictionary<int, Map_Info> m_mapInfoes = new Dictionary<int, Map_Info>();
    public Dictionary<int, Map_Info> MapInfoes { get { return m_mapInfoes; } }

    public void CreateAdventureMap(int maxMap = 5)
    {
        for(int i = 0; i < maxMap; i++)
        {
            CreateMapTile(i, "G1000");
        }
    }

    public void CreateDefenseMap(int maxMap = 0)
    {
        int mapIdx = 0;
        GameManager.CurGameMode = GameMode.Defense;

        Map_Info mapInfo = new Map_Info();
        mapInfo.mapCode = "H1000";

        m_mapInfoes.Add(mapIdx, mapInfo);
    }

    void CreateMapTile(int mapIdx, string mapCode)
    {
        m_mapInfoes[mapIdx].mapCode = mapCode;
    }

    void CreatePortal()
    {

    }

    void CreateEnemy()
    {
        
    }
}
