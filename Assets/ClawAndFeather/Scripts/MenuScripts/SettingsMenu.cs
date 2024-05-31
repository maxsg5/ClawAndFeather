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
        _buttons[1].onClick.AddListener(() => Singleton.Global.Audio.ChangeVolume(0.1f)); // increase volume
        _buttons[2].onClick.AddListener(() => Singleton.Global.Audio.ChangeVolume(-0.1f)); // reduce volume
    }
}
