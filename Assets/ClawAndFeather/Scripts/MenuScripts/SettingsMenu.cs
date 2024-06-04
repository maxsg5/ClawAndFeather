using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Settings Menu")]
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
