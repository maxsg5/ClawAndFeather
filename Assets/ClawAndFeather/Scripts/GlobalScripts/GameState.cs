using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public float Score { get; set; }
    public GameObject PlayerObject { get; private set; }
    public PlayerController Player { get; private set; }
    public bool Paused { get; private set; }
    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null)
        { Player = PlayerObject.GetComponent<PlayerController>(); }
    }

    /// <summary>
    /// Used to Flip the <see cref="GameState.Paused"/> state
    /// </summary>
    public void Pause() => Paused = !Paused;
    public static void ChangeScene(int ID) => SceneManager.LoadScene(ID);
    public static void ExitGame() => Application.Quit();
}
