using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameSceneUI : UI_Scene
{
    enum Texts
    {
        CardTimeText,
        Coin_Text,
        Level_CurLevelText,
    }

    enum Sliders
    {
        ExpBar,
    }

    enum Images
    {
        Treasure_0,
        Treasure_1,
        Treasure_2,
        Treasure_3,
    }

    enum Btns
    {
        Btn_Save,
    }

    private void Start()
    {
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Btns));

        GameManager.GameMgr.TimeEvent += ChangeCardTimeText;

        AddUIHandler(Get<Button>((int)Btns.Btn_Save).gameObject, ClickSaveBtn);
    }

    public void ChangeCardTimeText(int time)
    {
        Get<Text>((int)Texts.CardTimeText).text = time.ToString();
    }

    public void ChangeCoinText(int coin)
    {
        Get<Text>((int)Texts.Coin_Text).text = coin.ToString();
    }

    public void ChangeLevelText(int level)
    {
        Get<Text>((int)Texts.Level_CurLevelText).text = level.ToString();
    }

    public void ChangeExpBar(float percentage)
    {
        Get<Slider>((int)Sliders.ExpBar).value = percentage;
        Debug.Log(Get<Slider>((int)Sliders.ExpBar).value);
        Debug.Log(percentage);
    }

    public void ClickSaveBtn(PointerEventData evt)
    {
        GameManager.GameMgr.GamePause();
        Managers.UI.ShowPopupUI<UI_Save>(Defines.SceneType.GameScene);
    }

    public void ChnageTreasureImage(Sprite image, int idx)
    {

    }

    
}
