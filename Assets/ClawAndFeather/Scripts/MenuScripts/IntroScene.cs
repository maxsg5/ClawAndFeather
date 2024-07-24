using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Menu/Intro Scene")]
public class IntroScene : MonoBehaviour
{
    public void EndScene()
    {
        GameState.ChangeScene(2);
    }
}
