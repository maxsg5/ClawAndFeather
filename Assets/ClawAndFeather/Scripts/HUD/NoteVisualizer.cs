using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/HUD/Note Visualizer")]
public class NoteVisualizer : MonoBehaviour
{
    #region Inspector
    public Color offColour = Color.black;
    public Color onColour = Color.green;
    public Color restColour = Color.red;
    #endregion

    private Renderer _rend;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        _rend.material.color = Singleton.Global.Audio.CurrentChart.TryPlayNote(Singleton.Global.Audio.AudioTime, out var note, out _) 
            ? note.IsRest
                ? restColour // note detected, is rest.
                : onColour // note detected, is not rest.
            : offColour; // no note detected.
    }
}
