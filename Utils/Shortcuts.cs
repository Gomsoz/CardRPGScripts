using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shortcuts
{
    protected GameObject targetUI;
    public abstract void Execute();
}

public class Command_CharacterProfile : Shortcuts
{
    public override void Execute()
    {
        if(targetUI == null)
           targetUI = Managers.UI.GetSceneUI<UI_CharacterProfile>().gameObject;

        if (targetUI.gameObject.activeInHierarchy == false)
            targetUI.SetActive(true);
        else
            targetUI.SetActive(false);
    }
}

public class Command_Inventory : Shortcuts
{
    public override void Execute()
    {
        if (targetUI == null)
            targetUI = Managers.UI.GetSceneUI<UI_Inventory>().gameObject;

        if (targetUI.gameObject.activeInHierarchy == false)
            targetUI.SetActive(true);
        else
            targetUI.SetActive(false);
    }
}

public class Command_QuestInvenory : Shortcuts
{
    public override void Execute()
    {
        if (targetUI == null)
            targetUI = Managers.UI.GetSceneUI<UI_QuestScene>().gameObject;

        if (targetUI.gameObject.activeInHierarchy == false)
            targetUI.SetActive(true);
        else
            targetUI.SetActive(false);
    }
}
