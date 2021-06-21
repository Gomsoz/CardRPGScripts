using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scene_Defense : Scene_Game
{
    // 진입점 -> 맵, 오브젝트들의 생성, 저장, 로드를 해야함.
    /*
     * 맵을 먼저 생성하고 저장함.
     * 
     * 씬 관리
     */

    GameObject m_UIMaximumBonus = null;

    static Scene_Defense _instance;
    public static Scene_Defense Instance { get { return _instance; } }

    Map_Create m_mapCreate = new Map_Create();

    public Action SuppressorGaugeReachesTheMaximum = null;

    public override void AwakeInit()
    {
        if (_instance == null)
            _instance = this;

        sceneType = Defines.SceneType.DefenseScene;
        m_mapCreate.CreateDefenseMap();
        Managers.Board.LoadBoard(m_mapCreate.MapInfoes[0].mapCode);

        SuppressorGaugeReachesTheMaximum += ShowSelectBonusUI;
    }

    protected override void Init()
    {
        base.Init();
    }

    protected override void LoadData()
    {
        
    }

    protected override void NewLoadObject()
    {
        Defines.Position pos = new Defines.Position(2, 2);
        Transform player = Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Player, "Player", pos).transform;
        DontDestroyOnLoad(player);
        Managers.Camera.SetTrackingTarget(player);
        Managers.Camera.UpdateCameraPos(Defines.CameraType.Main);

        Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Object, "OnlyDefenseMode/ProtectedItem", new Defines.Position(3, 3));
        Managers.UI.ShowSceneUI<DefenseSceneUI>(sceneType);
        Managers.UI.ShowSceneUI<UI_MaximumBonus>(sceneType).gameObject.SetActive(false);
    }

    void CreateInitialObject()
    {

    }

    void ShowSelectBonusUI()
    {
        if (m_UIMaximumBonus == null)
            m_UIMaximumBonus = Managers.UI.GetSceneUI<UI_MaximumBonus>().gameObject;

        if (m_UIMaximumBonus.gameObject.activeInHierarchy == false)
            m_UIMaximumBonus.SetActive(true);
        else
            m_UIMaximumBonus.SetActive(false);
    }
}
