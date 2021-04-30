using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox_Creating : MonoBehaviour
{
    string m_prefabsPath = $"Prefabs/Object/Items/";
    Transform m_itemBoxHolder;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        m_itemBoxHolder = new GameObject { name = "ItemBoxHolder" }.transform;
    }

    public void InstantiateItemBox(Vector3 pos, Char_EnemyStats enemyStats)
    {
        GameObject go = Managers.Resources.Instantiate($"{m_prefabsPath}ItemBox_{enemyStats.enemyType}", m_itemBoxHolder);

        go.transform.position = pos;

        List<Defines.ItemType> items = new List<Defines.ItemType>();
        //items.Add((Defines.ItemType)UnityEngine.Random.Range(1, (int)Defines.ItemType.Count));
        items.Add(Defines.ItemType.HpPotion);

        go.GetComponent<ItemBoxBehavior>().AddItemInList(items, enemyStats); 
    }
}
