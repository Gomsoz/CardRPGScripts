using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedMapList
{
    List<string> portalMapIdx;

    // key : {WorldType}Idx_{Idx} 
    static Dictionary<string, List<string>> mapList = new Dictionary<string, List<string>>();

    public void SetLinkedMapList(string mapIdx)
    {
        mapList[mapIdx] = Managers.Json.LoadLinkedMapData(mapIdx);
    }

    public static string GetMapIdx(Defines.MapType type, int mapIdx, int portalIdx)
    {
        string key = $"{type}_{mapIdx}";
        List<string> list =  mapList[key];
        return list[portalIdx];
    }
}
