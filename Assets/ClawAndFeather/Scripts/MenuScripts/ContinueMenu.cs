using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Continue Menu")]
public class ContinueMenu : MenuController
{
    [Space]
    public int nextSceneBuildIndex;

    internal override void Awake()
    {
        base.Awake();

        _buttons[0].onClick.AddListener(NextScene); // next button
    }
    private void NextScene()
    {
        Singleton.Global.State.LimitedPause(false);
        GameState.ChangeScene(nextSceneBuildIndex);
    }
}
