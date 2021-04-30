using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBoarCannonBall : MonoBehaviour
{
    int m_ballSpeed = 10;
    int m_ballDamage = 20;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlyingBall(Managers.Object.Player.GetComponent<Char_PlayerCtr>().Position));
    }

    IEnumerator FlyingBall(Defines.Position pos)
    {
        Vector3 targetPos = Managers.Board.BoardPosToWorldPos(pos) + new Vector2(0, 1);
        Vector3 dir = targetPos - transform.position;

        while (dir.magnitude > 0.1f)
        {
            dir = targetPos - transform.position;
            yield return null;
            float moveDist = Mathf.Clamp(m_ballSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            collision.gameObject.GetComponent<Char_PlayerCtr>().UnderAttackedCharacter(m_ballDamage);
            Destroy(gameObject);
        }
    }
}
