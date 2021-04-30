using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UI_Scene
{
    GameObject m_damageUI;

    public override void Init()
    {
        base.Init();

        m_damageUI = Managers.Resources.Load<GameObject>($"Prefabs/UI/GameScene/Scene/Text_Damage");
    }

    public GameObject InstantiateDamageUI()
    {
        return GameObject.Instantiate(m_damageUI, transform);
    }

    public void ChangeDamageText(GameObject textUI, int value)
    {
        textUI.GetComponent<Text>().text = value.ToString();
    }
}
