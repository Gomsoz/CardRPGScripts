using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalMarkBehavior : MonoBehaviour
{
    [SerializeField]
    int m_portalIdx;

    private void Start()
    {
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            string nextScene = LinkedMapList.GetMapIdx(Managers.World.CurMapIdx, m_portalIdx);
            Managers.World.SetMapIdx(nextScene);
            string sceneName = nextScene.Split('_')[0];

            switch (sceneName)
            {
                case "MainWorld":
                    Managers.Scene.LoadScene(Defines.SceneType.GameScene);
                    break;
                case "SubMap":
                    Managers.Scene.LoadScene(Defines.SceneType.HouseScene);
                    break;
                default:
                    Debug.Log("Scene Name Error");
                    break;
            }
        }
    }

    public void Init(int idx)
    {
        m_portalIdx = idx;
    }
}
