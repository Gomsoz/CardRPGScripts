using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnPanel : UI_Scene
{
    enum Btns
    {
        Btn_Adventure,
        Btn_Character,
        Btn_Shop,
        Btn_Inventory,
        Btn_Option,
        Btn_Save,
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Btns));

        AddUIHandler(Get<Button>((int)Btns.Btn_Adventure).gameObject, ClickAdventureBtn);
    }

    public void ClickAdventureBtn(PointerEventData evt)
    {

    }

    public void ClickCharacterBtn(PointerEventData evt)
    {

    }

    public void ClickShopBtn(PointerEventData evt)
    {

    }

    public void ClickInventoryBtn(PointerEventData evt)
    {

    }

    public void ClickOptionBtn(PointerEventData evt)
    {

    }

    public void ClickSaveBtn(PointerEventData evt)
    {

    }
}
