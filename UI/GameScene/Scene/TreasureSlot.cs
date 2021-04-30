using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreasureSlot : UI_Base
{
    enum Images
    {
        TreasureImage,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<Image>(typeof(Images));
        Get<Image>((int)Images.TreasureImage).gameObject.SetActive(false);
    }

    public void ChangeTreasureImage(Sprite sprite)
    {
        Get<Image>((int)Images.TreasureImage).gameObject.SetActive(true);
        Get<Image>((int)Images.TreasureImage).sprite = sprite;
    }
}
