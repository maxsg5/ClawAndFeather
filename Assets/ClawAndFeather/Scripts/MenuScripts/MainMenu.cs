using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MenuController
{
    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => GameState.ChangeScene(1)); //play button
        _buttons[_buttonObjects.Length - 1].onClick.AddListener(GameState.ExitGame); // exit game
    }
}
