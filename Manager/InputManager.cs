using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputManager
{
    public Action keyboardAction = null;
    public Action MouseAction = null;

    Shortcuts button_I = new Command_Inventory();
    Shortcuts button_P = new Command_CharacterProfile();
    Shortcuts button_Q = new Command_QuestInvenory();

    public void OnUpdate()
    {
        if (Input.anyKey == true)
        {
            ChckUIShortcuts();
        }
    }

    void ChckUIShortcuts()
    {
        Shortcuts shortcut = null;

        if (Input.GetKeyDown(KeyCode.I))
            shortcut = button_I;
        else if (Input.GetKeyDown(KeyCode.P))
            shortcut = button_P;
        else if (Input.GetKeyDown(KeyCode.Q))
            shortcut = button_Q;

        if (shortcut == null)
            return;

        shortcut.Execute();
    }
}
