using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Treasure_base : MonoBehaviour
{
    protected Defines.TreasureType m_treasureType;
    public Defines.TreasureType Type { get { return m_treasureType; } }

    public abstract void Effect();
}
