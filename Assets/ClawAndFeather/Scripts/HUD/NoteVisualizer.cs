using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/HUD/Note Visualizer")]
public class NoteVisualizer : MonoBehaviour
{
    #region Inspector
    public Color offColour = Color.black;
    public Color earlyColour = Color.green;
    public Color lateColour = Color.red;
    #endregion

    private Renderer _rend;
    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = Singleton.Global.Audio;
        _rend = GetComponent<Renderer>();
    }

    void Update()
    {
        _rend.material.color = offColour;
        var time = _audioManager.AudioTime;
        if (_audioManager.CurrentChart.TryPlayNote(time, out var note, out _))
        {
            if (note.NoteTime < time)
            {
                _rend.material.color = earlyColour;
            }
            else if (note.NoteTime > time)
            {
                _rend.material.color = lateColour;
            }
        }
        else
        {
            Debug.Log("No note was played");
        }
    }
}
