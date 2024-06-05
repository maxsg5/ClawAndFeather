using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Credits Menu")]
public class CreditsMenu : MenuController
{
    [Space]
    public GameObject mainCanvas;

    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(() => StartCoroutine(Outro(2, mainCanvas))); //back button
    }
}
