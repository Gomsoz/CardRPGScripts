﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceClass
{
    public interface IPosition
    {
        Defines.Position Position { get; set; }

        void SetPosition(Defines.Position pos);
    }
}
