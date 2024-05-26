using UnityEngine;

public class GameState : MonoBehaviour
{
    public int Score { get; private set; }
    public PlayerController Player { get; private set; }
    void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
}
