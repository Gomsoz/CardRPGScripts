using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Game : Scene_Base
{

    private void Awake()
    {
        Managers.Board.AwakeInit();

        AwakeInit();
    }

    public virtual void AwakeInit()
    {
        Managers.World.SetMapIdx("MainWorld_0");
        sceneType = Defines.SceneType.GameScene;
        Managers.Board.LoadBoard("G1000");
    }

    protected override void Init()
    {
        base.Init();

        //LoadQuest();     
        Managers.Object.Init();
        Managers.Object.DyingEnemyEvent += SpawnEnemyConstantly;

        if (GameManager.GameMgr.IsLoadData == false)
            NewLoadObject();
        else
            LoadData();

        if (Managers.Instance.DontDestroyUIHolder.childCount == 0)
            DataLoadingFirsttime();


    }

    public void DataLoadingFirsttime()
    {
        LoadDontDestroyUI();
        Managers.Instance.LoadSlotManager();
    }

    public void LoadData()
    {
        int idx = GameManager.GameMgr.SaveSlotIdx;
        if(Managers.Object.Player == null)
            Managers.Json.LoadDataAndSpawnPlayer(idx);
        else
            Managers.Object.Player.GetComponent<Char_PlayerCtr>().SetPosition(new Defines.Position(m_mapData.SpawnPosX, m_mapData.SpawnPosY));

        Managers.Json.LoadDataAndSpawnEnemy(idx);
        Managers.Camera.BindCamera(Defines.CameraType.Main);
        Managers.Camera.SetTrackingTarget(Managers.Object.Player);
        Managers.Camera.UpdateCameraPos(Defines.CameraType.Main);
    }

    void NewLoadObject()
    {
        Defines.Position pos = new Defines.Position(3, 3);
        Transform player = Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Player, "Player", pos).transform;
        DontDestroyOnLoad(player);
        Managers.Camera.SetTrackingTarget(player);
        Managers.Camera.UpdateCameraPos(Defines.CameraType.Main);

        Managers.Object.SpawnEnemy("Pig", new Defines.Position(2, 3));
        Managers.Object.SpawnEnemy("PirateBoar", new Defines.Position(5, 3));
    }

    public virtual void LoadDontDestroyUI()
    {
        Managers.UI.DontDestroyUI<GameSceneUI>(sceneType);
        Managers.UI.DontDestroyUI<UI_Status>(sceneType);
        Managers.UI.DontDestroyUI<UI_CharacterProfile>(sceneType).gameObject.SetActive(false);
        Managers.UI.DontDestroyUI<Inventory_Treasure>(sceneType);
        Managers.UI.DontDestroyUI<UI_Inventory>(sceneType).gameObject.SetActive(false);
        Managers.UI.DisactivatePopupUI();

        Managers.Quest.InstantiateQuestUI();
    }

    void LoadQuest()
    {
        Quest_Hunt mainQuest = new Quest_Hunt("Pig", 1, QuestType.MainQuest);
        Managers.Quest.AddQuest(QuestType.MainQuest, mainQuest);

        Quest_Hunt subQuest = new Quest_Hunt("PirateBoar", 1, QuestType.SubQuest);
        Managers.Quest.AddQuest(QuestType.SubQuest, subQuest);
    }

    void SpawnEnemyConstantly(Char_EnemyStats stat)
    {
        Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Enemy, stat.Name, new Defines.Position(UnityEngine.Random.Range(0, Managers.Board.BoardWidth), UnityEngine.Random.Range(0, Managers.Board.BoardHeight)));
    }

    public override void Clear()
    {
        base.Clear();

        GameObject enemyHolder = GameObject.Find("EnemyHolder");
        for(int i = 0; i < enemyHolder.transform.childCount; i++)
        {
            enemyHolder.transform.GetChild(i).GetComponent<AI_BaseEnemy>().ClearTimeTracking();
        }
        Managers.Instance.Savedata();
        Managers.Json.SaveObjectData(m_mapData);
    }
}
