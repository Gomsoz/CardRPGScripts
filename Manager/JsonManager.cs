using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;

public class JsonManager
{
    int m_saveSlotIdx;

    public void SaveData()
    {
        m_saveSlotIdx = GameManager.GameMgr.SaveSlotIdx;
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

    void SaveEnemyData()
    {
        string loadString = File.ReadAllText("Assets/Resources/Data/EnemyData.json");
        JObject loaddata = JObject.Parse(loadString);

        JObject savedata = new JObject();

        JArray enemyStatsDatas = new JArray();
        JArray enemyPosDatas = new JArray();

        loaddata[$"EnemyData_{m_saveSlotIdx}"] = new JArray();
        loaddata[$"EnemyPos_{m_saveSlotIdx}"] = new JArray();

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

        savedata[$"EnemyData_{m_saveSlotIdx}"] = enemyStatsDatas;
        savedata[$"EnemyPos_{m_saveSlotIdx}"] = enemyPosDatas;

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
        string loadString = File.ReadAllText("Assets/Resources/Data/EnemyData.json");
        JObject loaddata = JObject.Parse(loadString);

        JArray enemyStatsDatas = (JArray)loaddata[$"EnemyData_{idx}"];
        JArray enemyPosDatas = (JArray)loaddata[$"EnemyPos_{idx}"];

        for (int i = 0; i < enemyStatsDatas.Count; i++)
        {
            Char_EnemyStats stats = JsonConvert.DeserializeObject<Char_EnemyStats>(loaddata[$"EnemyData_{idx}"][i].ToString());
            Defines.Position pos = JsonConvert.DeserializeObject<Defines.Position>(loaddata[$"EnemyPos_{idx}"][i].ToString());

            Transform enemy = Managers.Object.SpawnEnemy(stats.Name, pos);
            enemy.GetComponent<Char_EnemyCtr>().m_enemyStats = stats;
        }      
    }

    /*public void SaveScore(int level, int score)
    {
        JObject saveData = new JObject();

        string dataLevel = $"Level_{level}";

        saveData[dataLevel] = score;

        // 파일로 저장 
        string savestring = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        // JObject를 Serialize하여 json string 생성 
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
