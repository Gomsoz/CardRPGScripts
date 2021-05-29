using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Scene_Base : MonoBehaviour
{
    public Action<Defines.SceneType> SceneClearEvent = null;

    protected LinkedMapList m_linkedMapList = new LinkedMapList();
    protected Scene_MapData m_mapData;

    private void Start()
    {
        Init();
    }

    public Defines.SceneType sceneType { get; protected set; }

    protected virtual void Init()
    {
        Managers.Instance.DontDestroyUIHolder.gameObject.SetActive(true);
        GameManager.GameMgr.DontDestoryGameObject.gameObject.SetActive(true);

        UnityEngine.Object InstanceType = GameObject.FindObjectOfType(typeof(EventSystem));
        if (InstanceType == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";

        if(Managers.World.CurMapIdx != null)
        {
            m_linkedMapList.SetLinkedMapList(Managers.World.CurMapIdx);
            m_mapData = Managers.Json.LoadObjectData(Managers.World.CurMapIdx);

            if (m_mapData.PortalData != null)
            {
                for (int i = 0; i < m_mapData.PortalData.Count; i++)
                {
                    GameObject mark = Managers.Resources.Instantiate("Prefabs/Object/Mark/PortalMark");
                    mark.transform.position = Managers.Board.BoardPosToWorldPos(new Defines.Position(m_mapData.PortalData[i].PosX, m_mapData.PortalData[i].PosY));
                    mark.GetComponent<ProtalMarkBehavior>().Init(i);
                }
            }
        }   

        /*InstanceType = GameObject.FindObjectOfType(typeof(GameManager));
        if (InstanceType == null)
            Managers.Resources.Instantiate("Prefabs/GameManager").name = "@GameManager";*/
    }

    

    public virtual void Clear()
    {
        Managers.Instance.DontDestroyUIHolder.gameObject.SetActive(false);
        GameManager.GameMgr.DontDestoryGameObject.gameObject.SetActive(false);
    }
}
