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

        LoadUI();
    }

    protected override void Init()
    {
        base.Init();

        //LoadQuest();     
        Managers.Object.DyingEnemyEvent += SpawnEnemyConstantly;

        GameObject slot = Managers.Resources.Instantiate("Prefabs/Object/Card/CardSlot");
        slot.transform.SetParent(GameObject.Find("Main Camera").transform);

        if (GameManager.GameMgr.IsLoadData == false)
            NewLoadObject();
        else
            LoadData();

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
        Managers.Camera.SetTrackingTarget(player);
        Managers.Camera.UpdateCameraPos(Defines.CameraType.Main);

        pos = new Defines.Position(2, 3);
        Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Enemy, "Pig", pos);

        pos = new Defines.Position(5, 3);
        Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Enemy, "PirateBoar", pos);
    }

    void LoadUI()
    {
        Managers.UI.ShowSceneUI<GameSceneUI>(sceneType);
        Managers.UI.ShowSceneUI<UI_Status>(sceneType);
        Managers.UI.ShowSceneUI<UI_CharacterProfile>(sceneType).gameObject.SetActive(false);
        Managers.UI.ShowSceneUI<Inventory_Treasure>(sceneType);
        Managers.UI.ShowSceneUI<UI_Inventory>(sceneType).gameObject.SetActive(false);
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
    }
}
