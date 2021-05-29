using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager
{
    string m_curMapIdx;
    public string CurMapIdx { get { return m_curMapIdx; } }

    public void SetMapIdx(string mapIdx)
    {
        m_curMapIdx = mapIdx;
    }
}
