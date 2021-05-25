using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Factory_Enemy
{
    protected Dictionary<string, GameObject> m_enemyGOs = new Dictionary<string, GameObject>();
    protected string m_enemyPath = "Prefabs/Object/Enemy";

    public abstract GameObject CreateEnemy(string type, Defines.Position pos);

}

public class Factory_Pig : Factory_Enemy
{
    public void CreateEnemyForRandomPos(string type)
    {
        CreateEnemy(type, new Defines.Position(UnityEngine.Random.Range(0, Managers.Board.BoardWidth), UnityEngine.Random.Range(0, Managers.Board.BoardHeight)));
    }

    public override GameObject CreateEnemy(string enemyName, Defines.Position pos)
    {
        GameObject go;

        if(m_enemyGOs.TryGetValue(enemyName, out go) == false)
        {
            go = Managers.Resources.Load<GameObject>($"{m_enemyPath}/{enemyName}");
            if(go == null)
            {
                Debug.Log($"Faild to Create Enemy : {enemyName}");
                return null;
            }
            m_enemyGOs.Add(enemyName, go);
        }

        return GameObject.Instantiate(go); ; 
    }
}
