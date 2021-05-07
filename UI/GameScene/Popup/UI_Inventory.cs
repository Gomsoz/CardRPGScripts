using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inventory : UI_Scene
{
    enum GameObjects
    {
        Grid_ItemSlot,
        ItemInfo,
    }

    string m_prefabsPath = $"Prefabs/Object/Items/";
    Transform m_itemHolder;

    List<Transform> m_slots = new List<Transform>();
    List<Transform> m_items = new List<Transform>();

    int m_curDisplayIdx = -1;

    public override void Init()
    {
        Bind<Transform>(typeof(GameObjects));
        m_itemHolder = new GameObject { name = "ItemHolder" }.transform;
        Transform gridPanel = Get<Transform>((int)GameObjects.Grid_ItemSlot);

        for (int i = 0; i < gridPanel.childCount; i++)
        {
            Transform child = Utils.FindChild<Transform>(gridPanel.gameObject, $"InventorySlot_{i}", true);
            m_slots.Add(child);
            AddUIHandler(child.gameObject, ClickItem);
        }
    }

    public bool ChkAndAddItemInSlot(Transform item)
    {
        if (m_items.Count == m_slots.Count)
        {
            Debug.Log("인벤토리가 꽉 찼습니다.");
            return false;
        }

        m_slots[m_items.Count].GetComponent<ItemSlot>().ChangeItemImage(item.GetComponent<SpriteRenderer>().sprite);
        m_items.Add(item);
        return true;
    }

    public void ClickItem(PointerEventData evt)
    {
        GameObject go = evt.pointerCurrentRaycast.gameObject;

        Debug.Log($"Selected Slot {go.name}");
        m_curDisplayIdx = int.Parse(go.name.Split('_')[1]);

        if(m_curDisplayIdx >= m_items.Count)
        {
            Debug.Log($"아이템이 없습니다. : {m_curDisplayIdx} Slot");
            return;
        }
        Get<Transform>((int)GameObjects.ItemInfo).GetComponent<Invenotry_ItemInfo>().ChangeItemInfo(m_items[m_curDisplayIdx]);
    }

    public void DestroyItem()
    {
        if (m_items.Count > 1)
            SortItems();
            
        m_slots[m_curDisplayIdx].GetComponent<ItemSlot>().ChangeNoneItemImage();

        m_items.RemoveAt(m_curDisplayIdx);
        Get<Transform>((int)GameObjects.ItemInfo).GetComponent<Invenotry_ItemInfo>().ChangeNoneItemInfo();
    }

    void SortItems()
    {
        for(int i = m_curDisplayIdx; i < m_items.Count; i++)
        {
            m_slots[m_curDisplayIdx].GetComponent<ItemSlot>().ChangeItemImage
                (m_items[m_curDisplayIdx + 1].GetComponent<SpriteRenderer>().sprite);
        }
    }
}
