using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuController
{
    [Space]
    public GameObject settingsCanvas;
    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => GameState.ChangeScene(1)); //play button
        _buttons[1].onClick.AddListener(() => StartCoroutine(Outro(2, settingsCanvas))); // settings button
        _buttons[_buttonObjects.Length - 1].onClick.AddListener(GameState.ExitGame); // exit game
    }
}
