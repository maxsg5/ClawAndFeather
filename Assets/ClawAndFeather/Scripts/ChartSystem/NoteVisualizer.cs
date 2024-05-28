using UnityEngine;

public class NoteVisualizer : MonoBehaviour
{
    public TimeKeeper timeKeeper;
    [Space]
    public Color NoNote = Color.white;
    public Color Early = Color.yellow;
    public Color Note = Color.green;
    public Color Late = Color.red;

    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool noteHit = Singleton.Global.Audio.GetCurrentChart().CheckTolerance(timeKeeper.TimeUnpaused, out _, out var timeDiff);

        if (!noteHit)
        {
            rend.material.color = NoNote;
        }
        else if (timeDiff < 0)
        {
            rend.material.color = Early;
        }
        else if (timeDiff > 0)
        {
            rend.material.color = Late;
        }
        else
        {
            rend.material.color = Note;
        }
    }
}
