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

    // 씬이 로드될 때
    protected virtual void Init()
    {
        Managers.Instance.DontDestroyUIHolder.gameObject.SetActive(true);
        GameManager.GameMgr.DontDestoryGameObject.gameObject.SetActive(true);

        UnityEngine.Object InstanceType = GameObject.FindObjectOfType(typeof(EventSystem));
        if (InstanceType == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";

        // 맵 데이터를 로드함.
        /*if(Managers.World.CurMapIdx != null)
        {
            // 현재 맵에 연결된 다른 맵들의 정보
            m_linkedMapList.SetLinkedMapList(Managers.World.CurMapIdx);
            // 현재 맵 데이터
            m_mapData = Managers.Json.LoadObjectData(Managers.World.CurMapIdx);

            // 현재 맵의 포탈 생성
            if (m_mapData.PortalData != null)
            {
                for (int i = 0; i < m_mapData.PortalData.Count; i++)
                {
                    GameObject mark = Managers.Resources.Instantiate("Prefabs/Object/Mark/PortalMark");
                    mark.transform.position = Managers.Board.BoardPosToWorldPos(new Defines.Position(m_mapData.PortalData[i].PosX, m_mapData.PortalData[i].PosY));
                    mark.GetComponent<ProtalMarkBehavior>().Init(i);
                }
            }
        }  */ 

        /*InstanceType = GameObject.FindObjectOfType(typeof(GameManager));
        if (InstanceType == null)
            Managers.Resources.Instantiate("Prefabs/GameManager").name = "@GameManager";*/
    }

    public virtual void Clear()
    {
        // 모든 팝업을 닫는다
        Managers.UI.ClosePopupAll();

        // 로딩화면 때문에 DontDestroy 시킨 UI 와 Player를 비활성화 해주고, 로드가 될 때 다시 활성화 시켜줌.
        Managers.Instance.DontDestroyUIHolder.gameObject.SetActive(false);
        GameManager.GameMgr.DontDestoryGameObject.gameObject.SetActive(false);

        // 카메라 리스트 초기화
        Managers.Camera.ClearCameraList(); 
    }
}
