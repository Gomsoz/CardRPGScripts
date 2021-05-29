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
            Managers.World.SetMapIdx(LinkedMapList.GetMapIdx(Defines.MapType.MainWorld, 0, m_portalIdx));
            Managers.Scene.LoadScene(Defines.SceneType.HouseScene);
        }
    }

    public void Init(int idx)
    {
        m_portalIdx = idx;
    }
}
