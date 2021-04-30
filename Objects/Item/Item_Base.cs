using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item_Base : MonoBehaviour
{
    protected string m_itemName;
    protected string m_itemExplain;
    public string ItemName { get { return m_itemName; } }
    public string ItemExplain { get { return m_itemExplain; } }

    private void Start()
    {
        Init();
    }

    protected abstract void Init();

    public virtual void Use()
    {
        Debug.Log($"Item Used {transform.name}");
    }
}
