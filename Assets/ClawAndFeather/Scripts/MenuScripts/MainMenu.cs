using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/MainMenu")]
public class MainMenu : MenuController
{
    [Space]
    public GameObject settingsCanvas;
    public GameObject creditsCanvas;
    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => GameState.ChangeScene(1)); //play button
        _buttons[1].onClick.AddListener(() => StartCoroutine(Outro(2, settingsCanvas))); // settings button
        _buttons[2].onClick.AddListener(() => StartCoroutine(Outro(2, creditsCanvas))); // credits button
        _buttons[_buttonObjects.Length - 1].onClick.AddListener(GameState.ExitGame); // exit game
    }
}
