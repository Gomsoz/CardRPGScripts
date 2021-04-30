using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card_Interface
{
    public interface ICard_Attack
    {
        Defines.Position Position { get; set; }

        void SetPosition(Defines.Position pos);
    }
}
