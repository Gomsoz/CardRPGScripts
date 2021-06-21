using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectedItemBehavior : MonoBehaviour
{
    // 좌, 우, 상, 하, 좌상, 우상, 좌하, 우하
    int[] m_knockbackPosX = new int[8]
        {-1, 1, 0, 0, -1, 1, -1, 1};
    int[] m_knockbackPosY = new int[8]
        {0, 0, 1, -1, 1, 1, -1, -1};

    Object_Info m_info;
    [SerializeField]
    GameObject m_explodingEffect;

    private void Start()
    {
        m_info = GetComponent<Object_Info>();
        Scene_Defense.Instance.SuppressorGaugeReachesTheMaximum += ExplodingAroundProtected;
    }

    // 게이지가 가득 차면 주변 적을 밀어낸다.
    void ExplodingAroundProtected()
    {
        Defines.Position targetPos;

        m_explodingEffect.SetActive(true);
        for (int i = 0; i < m_knockbackPosX.Length; i++)
        {
            targetPos = new Defines.Position(m_knockbackPosX[i], m_knockbackPosY[i]);
            Transform character = Managers.Board.GetObjOnBoard(m_info.Position + targetPos);
            if (character == null)
                continue;

            targetPos += m_info.Position + targetPos;

            character.GetComponent<Char_BaseCtr>().CharacterMove(targetPos);
            Managers.Board.ChkMoveObjOnBoard(character, targetPos);
        }
    }

    void SelectMaximumGaugeBonus()
    {

    }
}
