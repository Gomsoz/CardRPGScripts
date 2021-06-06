using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Invenotry_ItemInfo : UI_Base
{
    enum Texts
    {
        NameText,
        ExplainText,
    }

    enum Buttons
    {
        UseButton,
    }

    Item_Base m_Item;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));

        AddUIHandler(Get<Button>((int)Buttons.UseButton).gameObject, ClickUseButton);
    }

    public void ChangeItemInfo(Transform item, bool isButton = true)
    {
        Item_Base _item = item.GetComponent<Item_Base>();

        if (_item == null)
            return;

        m_Item = _item;

        Get<Text>((int)Texts.NameText).text = _item.ItemName;
        Get<Text>((int)Texts.ExplainText).text = _item.ItemExplain;

        if (isButton == true)
            Get<Button>((int)Buttons.UseButton).gameObject.SetActive(true);
        else
            Get<Button>((int)Buttons.UseButton).gameObject.SetActive(false);
    }

    public void ClickUseButton(PointerEventData evt)
    {
        Debug.Log(evt);
        if (m_Item == null)
            return;

        m_Item.Use();
        Managers.UI.GetSceneUI<UI_Inventory>().DestroyItem();
    }

    public void ChangeNoneItemInfo()
    {
        Get<Text>((int)Texts.NameText).text = "";
        Get<Text>((int)Texts.ExplainText).text = "";
        Get<Button>((int)Buttons.UseButton).gameObject.SetActive(false);
    }
}
