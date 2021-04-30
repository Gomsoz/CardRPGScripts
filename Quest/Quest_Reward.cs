using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Reward
{
    string m_rewardText;
    public string RewardText { get { return m_rewardText; } }

    int m_rewardCoin;
    int m_rewardExp;

    public int RewardCoin { get { return m_rewardCoin; } }
    public int RewardExp { get { return m_rewardExp; } }

    public Quest_Reward(int coin, int exp)
    {
        m_rewardCoin = coin;
        m_rewardExp = exp;
        m_rewardText = $" + {m_rewardCoin} Coin, {m_rewardExp} Exp";
    }
}
