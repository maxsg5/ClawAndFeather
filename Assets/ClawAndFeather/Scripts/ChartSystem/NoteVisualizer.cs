using UnityEngine;

public class NoteVisualizer : MonoBehaviour
{
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
        
    }
}

public class BeatVisualizer : MonoBehaviour
{
    public Color onColour = Color.white;
    public Color offColour = Color.black;

    private void Update()
    {
        
    }
}