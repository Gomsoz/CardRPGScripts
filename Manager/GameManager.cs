using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    static Time_CardSystem _cardSystem;
    static MarkPoint _markPoint;
    static ItemBox_Creating _createItemBox;
    public static GameManager GameMgr { get { return _instance; } }
    public static Time_CardSystem TimeCardSystem { get { return _cardSystem; } }
    public static MarkPoint MarkPoint { get { return _markPoint; } }
    public static ItemBox_Creating CreateItembox { get { return _createItemBox; } }

    #region Event
    public Action<int> TimeEvent = null;
    public Action<Defines.CardTimeState> CardTimeState = null;
    #endregion

    int m_leftTime = 0;
    int m_saveSlotIdx = 0;
    public int SaveSlotIdx { get { return m_saveSlotIdx; } set { m_saveSlotIdx = value; } }
    public bool IsLoadData = false;

    private static bool paused = false;
    public static bool Paused
    {
        get { return paused; }
        set
        {
            paused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }
    private void Awake()
    {
        Singleton();
        _cardSystem = transform.GetComponent<Time_CardSystem>();
        _markPoint = transform.GetComponent<MarkPoint>();
        _createItemBox = GetComponent<ItemBox_Creating>(); 
    }

    private void Start()
    {
    }

    void Singleton()
    {
        if (_instance == null)
        {
            GameObject _MgrOb = GameObject.Find("@GameManager");
            if (_MgrOb == null)
            {
                _MgrOb = new GameObject { name = "@GameManager" };
                _MgrOb.AddComponent<Managers>();
            }
            _instance = _MgrOb.GetComponent<GameManager>();
            DontDestroyOnLoad(_MgrOb);
        }
    }

    public void GamePause()
    {
        Paused = !Paused;
        if (Paused)
        {
            //Managers.Sound.Pause(Defines.SoundType.Bgm);
            return;
        }
        //Managers.Sound.UnPause(Defines.SoundType.Bgm);
    }

    public void SetTiemer(int time)
    {
        m_leftTime = time;
        StartCoroutine(OnTimer());
    }

    IEnumerator OnTimer()
    {
        while (m_leftTime > 0)
        {
            yield return new WaitForSeconds(1f);
            m_leftTime--;
            TimeEvent.Invoke(m_leftTime);
        }
    }
}
