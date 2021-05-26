using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Loading : Scene_Base
{
    float m_loadingTime = 2f;
    public override void Clear()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoad(Managers.Scene.NextSceneName));
    }

    IEnumerator StartLoad(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        float timer = 0f;

        while(operation.isDone == false)
        {
            yield return null;
            timer += Time.deltaTime;
            if(operation.progress < 0.9f)
            {
                if(timer > m_loadingTime)
                {
                    timer = 0f;
                }
            }
            else
            {
                if (timer > m_loadingTime)
                {
                    operation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
  

    }
}
