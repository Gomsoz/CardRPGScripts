﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Scene_Base : MonoBehaviour
{
    private void Start()
    {
        Init();
    }

    public Defines.SceneType sceneType { get; protected set; }

    protected virtual void Init()
    {
        Object InstanceType = GameObject.FindObjectOfType(typeof(EventSystem));
        if (InstanceType == null)
            Managers.Resources.Instantiate("UI/EventSystem").name = "@EventSystem";

        /*InstanceType = GameObject.FindObjectOfType(typeof(GameManager));
        if (InstanceType == null)
            Managers.Resources.Instantiate("Prefabs/GameManager").name = "@GameManager";*/
    }

    public abstract void Clear();
}
