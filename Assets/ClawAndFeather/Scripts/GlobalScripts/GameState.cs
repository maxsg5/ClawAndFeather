using UnityEngine;

public class GameState : MonoBehaviour
{
    public float Score { get; set; }
    public PlayerController Player { get; private set; }
    public bool Paused { get; private set; }
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Used to Flip the <see cref="GameState.Paused"/> state
    /// </summary>
    public void Pause() => Paused = !Paused;
}
