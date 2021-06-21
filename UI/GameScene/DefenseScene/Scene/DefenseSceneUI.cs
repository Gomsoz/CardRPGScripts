using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefenseSceneUI : UI_Scene
{
    enum Sliders
    {
        Slider_SuppressorGauge,
    }

    private void Awake()
    {
        Bind<Slider>(typeof(Sliders));
    }
}
