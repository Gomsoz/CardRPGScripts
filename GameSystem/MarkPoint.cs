using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkPoint : MonoBehaviour
{
    List<Transform> m_listfMarkPoints = new List<Transform>();
    Transform m_markPoint;
    Defines.Position m_playerPos;

    Transform m_markPointHolder;

    Card_Base m_targetCard;

    private void Start()
    {
        m_markPoint = Managers.Resources.Load<Transform>("Prefabs/Object/Mark/SelectingMark");
        m_markPointHolder = new GameObject { name = "MarkPointHolder" }.transform;
    }

    private void Update()
    {
        if (m_listfMarkPoints.Count != 0)
            ChkMarkPointDir();

    }

    public void InstantiateMarkPoint(Card_Base card, List<Defines.Position> pos, bool isCopy = true)
    {
        m_targetCard = card;
        m_playerPos = Managers.Object.Player.GetComponent<Char_BaseCtr>().Position;
        for (int i = 0; i < pos.Count; i++)
        {
            GameObject go = GameObject.Instantiate(m_markPoint.gameObject, m_markPointHolder);
            go.GetComponent<SelectingMarkBehavior>().SetPosition(new Defines.Position(pos[i].posX, pos[i].posY));
            m_listfMarkPoints.Add(go.transform);
        }
    }

    void ChkMarkPointDir()
    {
        Defines.Direction targetDir = Defines.Direction.Left; 

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPos = Managers.Object.Player.position;

        Vector3 targetPos = playerPos - mousePos;
        targetPos.x = targetPos.x >= 0 ? targetPos.x : -targetPos.x;
        targetPos.y = targetPos.y >= 0 ? targetPos.y : -targetPos.y;

        //Debug.Log($"MousePos : {mousePos}");
        //Debug.Log($"playerPos : {playerPos}");
        //Debug.Log($"targetPos : {targetPos}");

        if (targetPos.x >= targetPos.y) // 좌, 우
        {
            if (playerPos.x - mousePos.x >= 0)
                targetDir = Defines.Direction.Left;
            else
                targetDir = Defines.Direction.Right;
        }
        else // 상, 하
        {
            if (playerPos.y - mousePos.y <= 0)
                targetDir = Defines.Direction.Up;
            else
                targetDir = Defines.Direction.Down;
        }
        ChangeMarkPoint(targetDir);
    }

    void ChangeMarkPoint(Defines.Direction dir)
    {
        foreach(var mark in m_listfMarkPoints)
        {
            mark.GetComponent<SelectingMarkBehavior>().SetPosition(dir);
        }
    }

    public void RemoveMarkPoint()
    {
        if (m_listfMarkPoints == null)
            return;

        foreach (Transform tr in m_listfMarkPoints)
        {
            GameObject.Destroy(tr.gameObject);
        }
        m_listfMarkPoints.Clear();
    }

    public void CallBackMarkPoint(Defines.Position pos)
    {
        m_targetCard.UsedWithMarkPoint(pos);
    }
}
