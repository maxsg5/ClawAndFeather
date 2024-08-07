using System.Collections;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Pause Menu")]
public class PauseMenu : MenuController
{
    [Space]
    public GameObject settingsCanvas;

    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => StartCoroutine(Unpause(2))); // unpause button
        _buttons[1].onClick.AddListener(() => StartCoroutine(Outro(2, settingsCanvas))); // settings menu
        _buttons[2].onClick.AddListener(MainMenu); // menu scene
    }
    private IEnumerator Unpause(float waitTime)
    {
        for (int i = 0; i < _buttonObjects.Length; i++)
        { _buttonAnimations[i].SetTrigger("Leave"); }
        yield return new WaitForSecondsRealtime(waitTime);
        Singleton.Global.State.Pause();
    }
    private void MainMenu()
    {
        Singleton.Global.State.LimitedPause();
        GameState.ChangeScene(0);
    }
}
