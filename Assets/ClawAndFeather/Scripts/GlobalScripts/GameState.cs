using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[AddComponentMenu("Scripts/Claw and Feather/Global/Game Manager")]
public class GameState : MonoBehaviour
{
    public float Score { get; set; }
    public GameObject PlayerObject { get; private set; }
    public PlayerController Player { get; private set; }

    [SerializeField] private GameObject _pausedMenu;
    public bool Paused { get; private set; }
    private int _currentBuildID;
    void Awake()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _currentBuildID = scene.buildIndex;
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null)
        { Player = PlayerObject.GetComponent<PlayerController>(); }
        if (_pausedMenu == null)
        { _pausedMenu = GameObject.FindGameObjectWithTag("PauseMenu"); }
        if (_pausedMenu != null)
        { _pausedMenu.SetActive(false); }
    }

    /// <summary>
    /// Used to Flip the <see cref="GameState.Paused"/> state.
    /// </summary>
    public void Pause()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0.0f : 1.0f;
        if (_pausedMenu != null)
        { _pausedMenu.SetActive(Paused); }
        Singleton.Global.Audio.PauseSong(Paused);
    }
    /// <summary>
    /// Exclusivly only flips <see cref="GameState.Paused"/> state.
    /// </summary>
    public void LimitedPause()
    {
        Paused = !Paused;
        Time.timeScale = Paused ? 0.0f : 1.0f;
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

    /// <summary>
    /// Never reference specifically
    /// </summary>
    /// <param name="context"></param>
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed && _currentBuildID == 0) // if on intro, can skip
        {
            switch (context.interaction)
            {
                case TapInteraction:
                    break;
                case HoldInteraction: // SKip
                    ChangeScene(_currentBuildID + 1);
                    break;
            }
        }
    }
}
