using System.Collections;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour
{
    public TimeKeeper timeKeeper;
    [Space]
    public Color noteColour = Color.green;
    public Color onColour = Color.white;
    public Color offColour = Color.black;
    public float flashTime = 1.0f;

    private bool _flash = true;
    private float _beatDelay;
    private Renderer _rend;

    private void Start()
    {
        _beatDelay = Singleton.Global.Audio.GetCurrentChart().BPM / 60;
        _rend = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (_flash)
        {
            StartCoroutine(Flash(
                delay: _beatDelay,
                flashTime: flashTime,
                noteDetected: Singleton.Global.Audio.GetCurrentChart().CheckTolerance(timeKeeper.TimeUnpaused, out _)));
        }
    }

    private IEnumerator Flash(float delay, float flashTime, bool noteDetected)
    {
        _flash = false;
        _rend.material.color = noteDetected ? noteColour : onColour;
        yield return new WaitForSeconds(flashTime);
        _rend.material.color = offColour;
        yield return new WaitForSeconds(delay - flashTime);
        _flash = true;
    }
}