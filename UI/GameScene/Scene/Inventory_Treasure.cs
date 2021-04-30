using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Treasure : UI_Scene
{
    enum TreasureList
    {
        //None,
        HP,
        Count,
    }

    enum GameObjects
    {
        GridPanel_TreasureSlot,
    }

    string m_prefabsPath = $"Prefabs/Object/Treasure/";
    Transform m_treasureHolder;

    List<Transform> m_slots = new List<Transform>();
    List<Transform> m_treasures = new List<Transform>();

    private void Start()
    {
        m_treasureHolder = new GameObject { name = "TreasureHolder" }.transform;

        Transform gridPanel = Get<Transform>((int)GameObjects.GridPanel_TreasureSlot);
        /*foreach(Transform tr in gridPanel.transform)
        {
            Destroy(tr.gameObject);
        }*/

        for (int i = 0; i < gridPanel.childCount; i++)
        {
            Transform child = Utils.FindChild<Transform>(gridPanel.gameObject, $"TreasureSlot_{i}", true);
            m_slots.Add(child);
        }
    }

    public override void Init()
    {
        base.Init();

        Bind<Transform>(typeof(GameObjects));  
    }

    public void RandomInstantiateTreasureObject()
    {
        string target = System.Enum.GetName(typeof(TreasureList), UnityEngine.Random.Range(0, (int)TreasureList.Count));
        Debug.Log(target);

        if (target == "None")
            return;

        GameObject go = Managers.Resources.Instantiate($"{m_prefabsPath}Treasure_{target}", m_treasureHolder);

        if (m_treasures.Contains(go.transform))
            return;

        m_slots[(int)m_treasures.Count].GetComponent<TreasureSlot>().ChangeTreasureImage(go.GetComponent<SpriteRenderer>().sprite);
        m_treasures.Add(go.transform);

        go.GetComponent<Treasure_base>().Effect();
    }
}
