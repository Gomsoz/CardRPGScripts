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
        mapList = new Dictionary<string, List<string>>();
        mapList[mapIdx] = Managers.Json.LoadLinkedMapData(mapIdx);
    }

    public static string GetMapIdx(string mapIdx, int portalIdx)
    {
        List<string> list =  mapList[mapIdx];
        return list[portalIdx];
    }
}
