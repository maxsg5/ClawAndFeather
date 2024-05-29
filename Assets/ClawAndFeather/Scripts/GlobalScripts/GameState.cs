using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public float Score { get; set; }
    public GameObject PlayerObject { get; private set; }
    public PlayerController Player { get; private set; }
    [field:SerializeField] public float TimeUnpaused { get; private set; }
    public bool Paused { get; private set; }
    void Awake()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        if (PlayerObject != null)
        { Player = PlayerObject.GetComponent<PlayerController>(); }
    }

    private void Update()
    {
        TimeUnpaused += Time.deltaTime;
    }

    /// <summary>
    /// Used to Flip the <see cref="GameState.Paused"/> state
    /// </summary>
    public void Pause() => Paused = !Paused;
}
