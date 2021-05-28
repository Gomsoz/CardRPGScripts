using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalMarkBehavior : MonoBehaviour
{
    int m_portalIdx;
    string m_mapCode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Managers.Scene.LoadScene(Defines.SceneType.HouseScene);
        }
    }

    public void SetPortalMark(string mapCode)
    {
        m_mapCode = mapCode;
    }
}
