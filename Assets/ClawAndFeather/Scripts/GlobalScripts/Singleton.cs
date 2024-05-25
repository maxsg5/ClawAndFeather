using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Global { get; private set; }
    public GameState State { get; private set; }
    void Awake()
    {
        if (Global != null && Global != this)
        { Destroy(gameObject); }
        else
        {
            Global = this;
            DontDestroyOnLoad(gameObject);
        }
        State = GetComponentInChildren<GameState>();
    }
}
