using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UI_Scene
{
    GameObject m_damageUI;

    int textTime = 0;

    public override void Init()
    {
        base.Init();

        m_damageUI = Managers.Resources.Load<GameObject>($"Prefabs/UI/Common/Text_Damage");
    }

    public GameObject InstantiateDamageUI()
    {
        return GameObject.Instantiate(m_damageUI, transform);
    }

    public void ChangeDamageText(GameObject textUI, int value)
    {
        textUI.GetComponent<Text>().text = value.ToString();

        if (textTime > 0)
            textTime = 0;
        else
            StartCoroutine(StartDamageText(textUI));
    }

    IEnumerator StartDamageText(GameObject textUI)
    {
        while(textTime < 10)
        {
            yield return new WaitForSeconds(0.1f);
            textTime++;
        }
        textUI.GetComponent<Text>().text = "";
        textTime = 0;
    }
}
