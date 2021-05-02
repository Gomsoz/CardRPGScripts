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

    enum Panel
    {
        UI_CardPanel,
    }

    private void Awake()
    {
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Btns));
        Bind<Transform>(typeof(Panel));

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

    public void ChangeCardImage(Defines.CardType type, int idx, Sprite image)
    {
        switch (type)
        {
            case Defines.CardType.Drawed:
                Get<Transform>((int)Panel.UI_CardPanel).GetComponent<UI_CardPanel>().ChangeDrawedCardImage(idx, image);
                break;
            case Defines.CardType.Enrolled:
                Get<Transform>((int)Panel.UI_CardPanel).GetComponent<UI_CardPanel>().ChangeEnrolledCardImage(idx, image);
                break;
        }
    }

    public void ChangeDefaultCardImage(Defines.CardType type, int idx)
    {
        switch (type)
        {
            case Defines.CardType.Drawed:
                Get<Transform>((int)Panel.UI_CardPanel).GetComponent<UI_CardPanel>().FlipTheDrawedCard(idx);
                break;
            case Defines.CardType.Enrolled:
                Get<Transform>((int)Panel.UI_CardPanel).GetComponent<UI_CardPanel>().FlipTheEnrolledCard(idx);
                break;
        }
    }

    public void ChnageTreasureImage(Sprite image, int idx)
    {

    }

    
}
