using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Game : Scene_Base
{

    private void Awake()
    {
        Managers.Board.AwakeInit();

        sceneType = Defines.SceneType.GameScene;
        Managers.Camera.BindCamera(Defines.CameraType.Main);

        Managers.Slot = GameObject.Find("Card_SlotManager").GetComponent<Card_SlotManager>();

        Managers.Board.LoadBoard("G1000");

        if(Managers.Instance.DontDestroyUIHolder.childCount == 0)
            DataLoadingFirsttime();

    }

    protected override void Init()
    {
        Managers.World.SetMapIdx("MainWorld_0");

        base.Init();

        //LoadQuest();     
        Managers.Object.Init();
        Managers.Object.DyingEnemyEvent += SpawnEnemyConstantly;

    }

    public void DataLoadingFirsttime()
    {
        if (GameManager.GameMgr.IsLoadData == false)
            NewLoadObject();
        else
            LoadData();

        LoadDontDestroyUI();
    }

    public void LoadData()
    {
        int idx = GameManager.GameMgr.SaveSlotIdx;
        Managers.Json.LoadDataAndSpawnPlayer(idx);
        Managers.Json.LoadDataAndSpawnEnemy(idx);
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
    }
}
