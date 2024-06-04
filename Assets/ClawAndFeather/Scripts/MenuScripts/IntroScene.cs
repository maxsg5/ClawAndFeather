using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/MenuScripts/IntroScene")]
public class IntroScene : MonoBehaviour
{
    public void EndScene()
    {
        GameState.ChangeScene(1);
    }
}
