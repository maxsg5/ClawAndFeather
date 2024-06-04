using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Global/Singleton")]
public class Singleton : MonoBehaviour
{
    public static Singleton Global { get; private set; }
    public GameState State { get; private set; }
    public AudioManager Audio { get; private set; }
    public PrefabManager Prefabs { get; private set; }
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
        Audio = GetComponentInChildren<AudioManager>();
        Prefabs = GetComponentInChildren<PrefabManager>();
    }
}
