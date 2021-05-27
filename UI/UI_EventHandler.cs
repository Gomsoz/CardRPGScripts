using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> OnClickHandler = null;

    UI_EventHandler _instance;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(eventData);
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }

    void Awake()
    {
        Singleton();
    }

    void Singleton()
    {
        if (_instance == null)
        {
            GameObject _MgrOb = GameObject.Find("@EventSystem");
            if (_MgrOb == null)
            {
                _MgrOb = new GameObject { name = "@EventSystem" };
                _MgrOb.AddComponent<Managers>();
            }
            _instance = _MgrOb.GetComponent<UI_EventHandler>();
            DontDestroyOnLoad(_MgrOb);
        }
    }
}
