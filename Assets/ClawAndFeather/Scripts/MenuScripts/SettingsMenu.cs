using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MenuController
{
    [Space]
    public GameObject mainCanvas;

    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => StartCoroutine(Outro(2, mainCanvas))); //back button
    }
}
