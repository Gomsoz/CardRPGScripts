using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtalMarkBehavior : MonoBehaviour
{
    int mapCode;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Managers.Scene.LoadScene(Defines.SceneType.HouseScene);
        }
    }
}
