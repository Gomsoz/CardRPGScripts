using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public Scene_Base CurrentScene { get { return GameObject.FindObjectOfType<Scene_Base>(); } }
    string m_nextSceneName;
    public string NextSceneName { get { return m_nextSceneName; } }

    public Action<Defines.SceneType> SceneClearEvent = null;
    public Action<Defines.SceneType> SceneLoadEvent = null;

    public void LoadScene(Defines.SceneType sceneType)
    {
        Debug.Log(CurrentScene);
        CurrentScene.Clear();
        m_nextSceneName = System.Enum.GetName(typeof(Defines.SceneType), sceneType);

        if (!m_nextSceneName.Contains("Scene"))
            m_nextSceneName = $"{m_nextSceneName}Scene";

        SceneManager.LoadScene(System.Enum.GetName(typeof(Defines.SceneType), Defines.SceneType.LoadingScene));
    }
}
