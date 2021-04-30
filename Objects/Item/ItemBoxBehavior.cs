using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBoxBehavior : MonoBehaviour
{
    List<Defines.ItemType> m_items = new List<Defines.ItemType>();
    Char_EnemyStats m_enemyStats;

    string m_prefabsPath = $"Prefabs/Object/Items/";

    public void AddItemInList(List<Defines.ItemType> itemList, Char_EnemyStats stat)
    {
        foreach (Defines.ItemType type in itemList)
            m_items.Add(type);
        m_enemyStats = stat;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Player")
        {
            foreach (Defines.ItemType type in m_items)
            {
                Debug.Log(type);
                GiveItemToPlayer(type, collision);
            }

            GameObject.Destroy(gameObject);
        }
    }

    void GiveItemToPlayer(Defines.ItemType type, Collider2D col)
    {
        switch (type)
        {
            case Defines.ItemType.Coin:
                col.GetComponent<Char_PlayerCtr>().AddCoin(m_enemyStats.Drop_Coin);
                break;
            case Defines.ItemType.ExpPotion:
                col.GetComponent<Char_PlayerCtr>().AddExp(m_enemyStats.Drop_Exp);
                break;
            case Defines.ItemType.HpPotion:
                InstantiateAndAddItemOnUI(type);
                break;
            case Defines.ItemType.MpPotion:
                InstantiateAndAddItemOnUI(type);
                break;
            case Defines.ItemType.RandomReineforcementScroll:
                InstantiateAndAddItemOnUI(type);
                break;
            case Defines.ItemType.SelectReinforecementScroll:
                InstantiateAndAddItemOnUI(type);
                break;
            case Defines.ItemType.NormalTreasure:
                Managers.UI.GetSceneUI<Inventory_Treasure>().RandomInstantiateTreasureObject();
                break;
            case Defines.ItemType.MonsterTreasure:
                break;
        }
    }

    void InstantiateAndAddItemOnUI(Defines.ItemType type)
    {
        GameObject go = Managers.Resources.Instantiate($"{m_prefabsPath}Item_{type}");
        Managers.UI.GetSceneUI<UI_Inventory>().ChkAndAddItemInSlot(go.transform);
    }
}
