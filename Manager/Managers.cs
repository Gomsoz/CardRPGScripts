using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    #region Property
    static Managers _instance;
    static BoardManager _board = new BoardManager();
    static CameraManager _camera = new CameraManager();
    static JsonManager _json = new JsonManager();
    static ObjectManager _object = new ObjectManager();
    static UIManager _ui = new UIManager();
    static InputManager _input = new InputManager();
    static ResourcesManager _resources = new ResourcesManager();
    static SceneManagerEx _scene = new SceneManagerEx();
    static QuestManager _quest = new QuestManager();
    static Card_SlotManager _slot;

    public static Managers Instance { get { return _instance; } }
    public static BoardManager Board { get { return _board; } }
    public static CameraManager Camera { get { return _camera; } }
    public static JsonManager Json { get { return _json; } }
    public static ObjectManager Object { get { return _object; } }
    public static UIManager UI { get { return _ui; } }
    public static InputManager Input { get { return _input; } }
    public static ResourcesManager Resources { get { return _resources; } }
    public static SceneManagerEx Scene { get { return _scene; } }
    public static QuestManager Quest { get { return _quest; } }

    public static Card_SlotManager Slot;
    #endregion

    public Transform MainCamera;
    public Transform tempItem;
    public Transform DontDestroyUIHolder;

    private void Awake()
    {
        Singleton();
        ManagersInit(); 
    }

    private void Update()
    {
        _input.OnUpdate();
    }

    void Singleton()
    {
        if (_instance == null)
        {
            GameObject _MgrOb = GameObject.Find("@Managers");
            if (_MgrOb == null)
            {
                _MgrOb = new GameObject { name = "@Managers" };
                _MgrOb.AddComponent<Managers>();
            }
            _instance = _MgrOb.GetComponent<Managers>();
            DontDestroyOnLoad(_MgrOb);
        }
    }

    void ManagersInit()
    {
        _ui.UIMnangerInit();
        if (DontDestroyUIHolder == null)
        {
            DontDestroyUIHolder = new GameObject { name = "DontDestroyUIHolder" }.transform;
            DontDestroyOnLoad(DontDestroyUIHolder);
        }
    }

    public void Savedata()
    {
        Managers._json.SaveData();
    }
}
