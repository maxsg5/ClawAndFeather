using System.Collections;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour
{
    public Color noteColour = Color.green;
    public Color onColour = Color.white;
    public Color offColour = Color.black;
    public float flashTime = 1.0f;

    private bool _isOn = false;
    private float _bpm;
    private Renderer _rend;

    private void Start()
    {
        _bpm = Singleton.Global.Audio.GetCurrentChart().BPM;
        _rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (!_isOn)
        {
            _isOn = true;
            StartCoroutine(Flash(flashTime, Singleton.Global.Audio.GetCurrentChart().CheckTolerance());
        }
    }

    private IEnumerator Flash(float flashTime, bool noteDetected)
    {
        _rend.material.color = noteDetected ? noteColour : onColour;
        yield return new WaitForSeconds(flashTime);

    }
}