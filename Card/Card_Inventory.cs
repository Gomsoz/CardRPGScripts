using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Inventory : MonoBehaviour
{
    List<Transform> m_listOfCardsWhohave = new List<Transform>();
    Queue<Transform> m_playerDeck = new Queue<Transform>();

    public Transform[] testCards = new Transform[5];

    private void Awake()
    {
        SaveToCardList(testCards[0]);
        SaveToCardList(testCards[1]);
        SaveToCardList(testCards[2]);
        SaveToCardList(testCards[3]);
        SaveToCardList(testCards[4]);
    }

    public Transform GetCard()
    {
        if (m_playerDeck.Count == 0)
        {
            SetPlayerDeckFromList();
            ShufflePlayerDeck();
        }

        return m_playerDeck.Dequeue();
    }

    void SetPlayerDeckFromList()
    {
        for(int i = 0; i < m_listOfCardsWhohave.Count; i++)
        {
            m_playerDeck.Enqueue(m_listOfCardsWhohave[i]);
        }
    }

    public void SaveToCardList(Transform card)
    {
        card.GetComponent<Card_Base>().SetCardInfo();
        m_listOfCardsWhohave.Add(card);
        m_playerDeck.Enqueue(card);
        ShufflePlayerDeck();
    }

    public void ShufflePlayerDeck()
    {
        Transform[] postDeck = new Transform[m_playerDeck.Count];
        m_playerDeck.CopyTo(postDeck, 0);
        m_playerDeck.Clear();

        int random_1 = 0;
        int random_2 = 0;
        Transform temp;

        for (int i = 0; i < postDeck.Length; i++)
        {
            random_1 = UnityEngine.Random.Range(0, postDeck.Length);
            random_2 = UnityEngine.Random.Range(0, postDeck.Length);

            temp = postDeck[random_1];
            postDeck[random_1] = postDeck[random_2];
            postDeck[random_2] = temp;
        }

        for(int i = 0; i < postDeck.Length; i++)
        {
            m_playerDeck.Enqueue(postDeck[i]);
        }
    }   
}
