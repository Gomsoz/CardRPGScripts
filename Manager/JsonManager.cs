using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

public class JsonManager
{
    int m_saveSlotIdx;
    string m_curMapIdx;

    public void SaveData()
    {
        m_saveSlotIdx = GameManager.GameMgr.SaveSlotIdx;
        m_curMapIdx = Managers.World.CurMapIdx;
        SavePlayerData();
        SaveEnemyData();
    }

    void SavePlayerData()
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/PlayerData.json");
        JObject loaddata = JObject.Parse(loadString);

        JObject savedata = new JObject();

        Char_PlayerStats playerData = Managers.Object.Player.GetComponent<Char_PlayerCtr>().PlayerStats;

        savedata[$"isSave_{m_saveSlotIdx}"] = true;
        savedata[$"PlayerData_{m_saveSlotIdx}"] = JToken.FromObject(playerData);

        Char_CommonStats DefaultStat = Managers.Object.Player.GetComponent<Char_PlayerCtr>().DefaultStats;
        savedata[$"DefaultStat_{m_saveSlotIdx}"] = JToken.FromObject(DefaultStat);

        Char_CommonStats AdditionalStat = Managers.Object.Player.GetComponent<Char_PlayerCtr>().AdditionalStats;
        savedata[$"AdditionalStat_{m_saveSlotIdx}"] = JToken.FromObject(AdditionalStat);

        savedata[$"PlayerPos_{m_saveSlotIdx}"] = JToken.FromObject(Managers.Object.Player.GetComponent<Char_PlayerCtr>().Position);

        loaddata.Merge(savedata);

        string savestring = JsonConvert.SerializeObject(loaddata, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Data/PlayerData.json", savestring);
    }



    public void SaveInitialEnemyData(Map_Info info)
    {
        JObject saveData = new JObject();

        JArray enemyStatsDatas = new JArray();
        JArray enemyPosDatas = new JArray();

        // 파일로 저장 
        string savestring = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        // JObject를 Serialize하여 json string 생성 
        File.WriteAllText("Assets/Resources/Data/ScoreData.json", savestring); // 생성된 string을 파일에 쓴다 }
    }
    void SaveEnemyData()
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/EnemyData.json");
        JObject loaddata = JObject.Parse(loadString);

        JObject savedata = new JObject();

        JArray enemyStatsDatas = new JArray();
        JArray enemyPosDatas = new JArray();

        loaddata[$"EnemyData_{m_saveSlotIdx}"][m_curMapIdx] = new JArray();
        loaddata[$"EnemyPos_{m_saveSlotIdx}"][m_curMapIdx] = new JArray();

        GameObject enemyHolder = GameObject.Find("EnemyHolder");
        Char_EnemyStats enemyStats;
        Defines.Position enemyPos;

        for(int i = 0; i < enemyHolder.transform.childCount; i++)
        {
            Char_EnemyCtr enemy = enemyHolder.transform.GetChild(i).GetComponent<Char_EnemyCtr>();
            enemyStats = enemy.m_enemyStats;
            enemyStatsDatas.Add(JToken.FromObject(enemyStats));
            enemyPos = enemy.Position;
            enemyPosDatas.Add(JToken.FromObject(enemyPos));
        }

        savedata[$"ObjectData_{m_saveSlotIdx}"] = new JObject();
        savedata[$"EnemyData_{m_saveSlotIdx}"][$"{m_curMapIdx}"] = enemyStatsDatas;
        savedata[$"EnemyPos_{m_saveSlotIdx}"][$"{m_curMapIdx}"] = enemyPosDatas;
  

        loaddata.Merge(savedata);

        string savestring = JsonConvert.SerializeObject(loaddata, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Data/EnemyData.json", savestring);

        
    }

    public Char_PlayerStats LoadPlayerData(int idx)
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/PlayerData.json");
        JObject loaddata = JObject.Parse(loadString);

        if ((bool)loaddata[$"isSave_{idx}"] == false)
            return null;

        return JsonConvert.DeserializeObject<Char_PlayerStats>(loaddata[$"PlayerData_{idx}"].ToString());
    }

    public void LoadDataAndSpawnPlayer(int idx)
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/PlayerData.json");
        JObject loaddata = JObject.Parse(loadString);

        Char_PlayerStats playerStats = JsonConvert.DeserializeObject<Char_PlayerStats>(loaddata[$"PlayerData_{idx}"].ToString());
        Char_CommonStats defaultStats = JsonConvert.DeserializeObject<Char_PlayerStats>(loaddata[$"DefaultStat_{idx}"].ToString());
        Char_CommonStats additionalStats = JsonConvert.DeserializeObject<Char_PlayerStats>(loaddata[$"AdditionalStat_{idx}"].ToString());
        Defines.Position pos = JsonConvert.DeserializeObject<Defines.Position>(loaddata[$"PlayerPos_{idx}"].ToString());

        Transform player = Managers.Object.SpawnObjectOnBoard(Defines.ObjectType.Player, "Player", pos);
        player.GetComponent<Char_PlayerCtr>().SetPlayerData(playerStats, defaultStats, additionalStats);
    }

    public void LoadDataAndSpawnEnemy(int idx)
    {
        m_curMapIdx = Managers.World.CurMapIdx;

        string loadString = File.ReadAllText("Assets/Resources/Data/EnemyData.json");
        JObject loaddata = JObject.Parse(loadString);  

        JArray enemyStatsDatas = (JArray)loaddata[$"EnemyData_{idx}"][m_curMapIdx];
        JArray enemyPosDatas = (JArray)loaddata[$"EnemyPos_{idx}"][m_curMapIdx];

        for (int i = 0; i < enemyStatsDatas.Count; i++)
        {
            Char_EnemyStats stats = JsonConvert.DeserializeObject<Char_EnemyStats>(enemyStatsDatas[i].ToString());
            Defines.Position pos = JsonConvert.DeserializeObject<Defines.Position>(enemyPosDatas[i].ToString());

            Transform enemy = Managers.Object.SpawnEnemy(stats.Name, pos);
            enemy.GetComponent<Char_EnemyCtr>().m_enemyStats = stats;
        }      
    }

    public void SaveObjectData(Scene_MapData mapData)
    {
        int slotIdx = 0;
        m_curMapIdx = Managers.World.CurMapIdx;

        string loadString = File.ReadAllText("Assets/Resources/Data/ObjectData.json");
        JObject loaddata = JObject.Parse(loadString);

        JObject savedata = new JObject();

        GameObject objectHolder = GameObject.Find("ObjectHolder");

        JArray portalDatas = (JArray)loaddata[$"ObjectData_{slotIdx}"][m_curMapIdx]["PortalData"];
        portalDatas = new JArray();

        slotIdx = 2;

        savedata[$"ObjectData_{slotIdx}"] = new JObject();
        savedata[$"ObjectData_{slotIdx}"][m_curMapIdx] = (JObject)JToken.FromObject(mapData);

        loaddata.Merge(savedata);

        string savestring = JsonConvert.SerializeObject(loaddata, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Data/ObjectData.json", savestring);
    }

    public Scene_MapData LoadObjectData(string mapIdx)
    {
        string[] parsing = mapIdx.Split('_');
        string mapType = parsing[0];
        int mapNum = int.Parse(parsing[1]);
        int slotIdx = GameManager.GameMgr.SaveSlotIdx;

        string loadString = File.ReadAllText("Assets/Resources/Data/ObjectData.json");
        JObject loaddata = JObject.Parse(loadString);

        Scene_MapData mapData = new Scene_MapData();
        mapData = JsonConvert.DeserializeObject<Scene_MapData>(loaddata[$"ObjectData_{slotIdx}"][$"{mapType}_{mapNum}"].ToString());

        JArray objectDatas = (JArray)loaddata[$"ObjectData_{slotIdx}"][$"{mapType}_{mapNum}"]["PortalData"];

        for(int i = 0; i < objectDatas.Count; i++)
        {
            mapData.PortalData.Add(JsonConvert.DeserializeObject<Portal_Info>(objectDatas[i].ToString()));
        }

        return mapData;
    }

    public List<string> LoadLinkedMapData(string mapIdx)
    {
        string[] parsing = mapIdx.Split('_');
        string mapType = parsing[0];
        int mapNum = int.Parse(parsing[1]);
        int slotIdx = GameManager.GameMgr.SaveSlotIdx;

        string loadString = File.ReadAllText("Assets/Resources/Data/LinkedMapData.json");
        JObject loaddata = JObject.Parse(loadString);

        JArray linkedMapDatas = (JArray)loaddata[$"LinkMapData_{slotIdx}"][$"{mapType}_{mapNum}"];

        List<string> list = new List<string>();

        for (int i = 0; i < linkedMapDatas.Count; i++)
        {
            list.Add(linkedMapDatas[i].ToString());
        }

        return list;
    }

    public bool LoadBoardData(string code, out Board_Base board)
    {
        Debug.Log("Load Data... : Start Load the Board Data");
        string loadString = File.ReadAllText("Assets/Resources/Data/MapCodeData.json");

        if (loadString == null)
        {
            board = null;
            return false;
        }

        JObject loaddata = JObject.Parse(loadString); // JObject 파싱 

        JArray loadarray = (JArray)loaddata[code]["Tiles"];
        int[,] tileIndexs = new int[(int)loaddata[code]["Width"] + 1, (int)loaddata[code]["Height"] + 1];

        int dataCnt = 0;
        for (int i = 1; i < (int)loaddata[code]["Width"] + 1; i++)
        {
            for (int j = 1; j < (int)loaddata[code]["Height"] + 1; j++)
            {
                tileIndexs[i, j] = (int)loadarray[dataCnt];
                dataCnt++;
            }
        }

        board = Board_Base.Builder()
                .SetWidth((int)loaddata[code]["Width"])
                .SetHeight((int)loaddata[code]["Height"])
                .SetTileIndexs(tileIndexs)
                .BoardBuild();
            
        return true;
    }

    #region Defense Mode
    public void SaveDefenseModeData(Map_Info mapInfo)
    {
        SaveDefenseMapData(mapInfo.mapCode);
    }

    public void LoadDefenseModeData()
    {
        LoadDefenseMapData();
    }

    void SaveDefenseMapData(string mapCode)
    {
        JObject saveData = new JObject();

        string _curGameMode = Enum.GetName(typeof(GameMode), GameManager.CurGameMode);
        saveData["Mode"] = _curGameMode;
        saveData["MapCode"] = mapCode;

        JArray _mapcodes = new JArray();

        string savestring = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Data/MapData.json", savestring); 
    }

    void SaveDefenseObjectData()
    {

    }

    void SaveDefenseEnemyData()
    {

    }

    void LoadDefenseMapData()
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/MapData.json");

        if (loadString == null)
        {
            Debug.Log($"Cannot Find Map Data");
            return;
        }

        JObject loaddata = JObject.Parse(loadString);

        string _mapCode = (string)loaddata["MapCode"];

        GameManager.CurGameMode = GameMode.Defense;
        Managers.Board.LoadBoard(_mapCode);

    }
    #endregion

    /*public void SaveScore(int level, int score)
    {
        JObject saveData = new JObject();

        string dataLevel = $"Level_{level}";

        saveData[dataLevel] = score;

        // 파일로 저장 
        string savestring = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText("Assets/Resources/Data/ScoreData.json", savestring); // 생성된 string을 파일에 쓴다 }

        //출처: https://blog.komastar.kr/232 [World of Komastar]
    }*/

    /*public int LoadScore(int level)
    {

        string loadString = File.ReadAllText("Assets/Resources/Data/ScoreData.json");
        JObject loadData = JObject.Parse(loadString);

        int score = 0;
        string dataLevel = $"Level_{level}";

        score = (int)loadData[dataLevel];

        return score;
    }*/

    /*public List<string> LoadStageLevelData(string enemyName)
    {
        Debug.Log("Load Data... : Start Load the Data");
        string loadString = File.ReadAllText("Assets/Resources/Data/StageLevelData.json");
        JObject loaddata = JObject.Parse(loadString); // JObject 파싱 

        string targetLevel = $"Level_{level}";

        StageLevelData levelData = new StageLevelData();
        levelData.numOfBlockColors = (int)loaddata[targetLevel]["numOfBlockColors"];
        levelData.numOfNoneBlocks = (int)loaddata[targetLevel]["numOfNoneBlocks"];
        levelData.numOfGoalLines = (int)loaddata[targetLevel]["numOfGoalLines"];
        levelData.boardSize = (int)loaddata[targetLevel]["boardSize"];

        JArray loadarray = (JArray)loaddata[targetLevel]["boardMap"];
        levelData.boardMap = new int[levelData.boardSize + 1, levelData.boardSize + 1];

        int dataCnt = 0;
        for (int i = 1; i < levelData.boardSize + 1; i++)
        {
            for (int j = 1; j < levelData.boardSize + 1; j++)
            {
                levelData.boardMap[i, j] = (int)loadarray[dataCnt];
                dataCnt++;
            }
        }

        Debug.Log("Load Data... : Finish Load the Data");
        return levelData;

    }
    //출처: https://blog.komastar.kr/232 [World of Komastar]*/
}
