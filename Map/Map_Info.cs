using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Info
{
    // 맵 상의 오브젝트 위치
    public List<Object_Info> ObjectsOnMap = new List<Object_Info>();

    // 맵 상의 포탈 위치 정보
    public List<Portal_Info> PortalPositionsOnMap = new List<Portal_Info>();


    public string mapCode;
    //Dictionary<int>
}
