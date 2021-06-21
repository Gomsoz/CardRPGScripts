using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Info : MonoBehaviour, InterfaceClass.IPosition
{
    Defines.Position m_position;
    ObjectType m_objectType;
    string m_objectName;

    public Defines.Position Position { get { return m_position; } }
    
    public void SetObjectInfo()
    {

    }

    public void SetPosition(Defines.Position pos)
    {
        m_position = pos;
        transform.position = Managers.Board.BoardPosToWorldPos(m_position) + new Vector2(0, 1);
    }
}
