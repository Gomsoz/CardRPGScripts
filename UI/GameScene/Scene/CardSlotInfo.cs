using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSlotInfo : MonoBehaviour
{
    [SerializeField]
    Defines.CardType slotType;

    public Defines.CardType SlotType { get { return slotType; } }

    [SerializeField]
    int slotIdx;

    public int SlotIdx { get { return slotIdx; } }
}
