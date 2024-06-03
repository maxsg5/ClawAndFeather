using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public float Score { get; set; }
    public GameObject PlayerObject { get; private set; }
    public PlayerController Player { get; private set; }

    [SerializeField] private GameObject _pausedMenu;
    public bool Paused { get; private set; }
    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null)
        { Player = PlayerObject.GetComponent<PlayerController>(); }
        if (_pausedMenu == null)
        { _pausedMenu = GameObject.FindGameObjectWithTag("PauseMenu"); }
        if (_pausedMenu != null)
        { _pausedMenu.SetActive(false); }
    }

    /// <summary>
    /// Used to Flip the <see cref="GameState.Paused"/> state
    /// </summary>
    public void Pause()
    {
        Debug.Log("Called");
        Paused = !Paused;
        if (_pausedMenu != null)
        {  _pausedMenu.SetActive(Paused); }
        Singleton.Global.Time.SetGameTime(Paused ? 0.0f : 1.0f);
        Debug.Log(Singleton.Global.Time.GameTime);
    }

    /// <summary>
    /// Give the <see cref="SceneManager"/> build index to choose what <paramref name="ID"/> to change to.
    /// </summary>
    /// <param name="ID"></param>
    public static void ChangeScene(int ID) => SceneManager.LoadScene(ID);

    /// <summary>
    /// This is pretty self-explanatory, stupid.
    /// </summary>
    public static void ExitGame() => Application.Quit();
}
