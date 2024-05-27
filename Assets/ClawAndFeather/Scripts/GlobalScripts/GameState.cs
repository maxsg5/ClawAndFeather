using UnityEngine;

public class GameState : MonoBehaviour
{
    public float Score { get; private set; }
    public PlayerController Player { get; private set; }
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    /// <summary>
    /// Used to Set the Score of the <see cref="GameState.Score"/>
    /// </summary>
    /// <param name="score"></param>
    public void SetScore(float score) => Score = score;
}
