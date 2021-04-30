using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : UI_Base 
{
    enum Images
    {
        ItemImage,
    }

    private void Awake()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.ItemImage).gameObject.SetActive(false);
    }

    public void ChangeItemImage(Sprite sprite)
    {
        Get<Image>((int)Images.ItemImage).gameObject.SetActive(true);
        Get<Image>((int)Images.ItemImage).sprite = sprite;
    }

    public void ChangeNoneItemImage()
    {
        Get<Image>((int)Images.ItemImage).gameObject.SetActive(false);
    }
}
