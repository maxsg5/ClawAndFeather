using System.Collections;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour
{
    public Color noteColour = Color.green;
    public Color onColour = Color.white;
    public Color offColour = Color.black;
    public float flashTime = 1.0f;

    private TimeKeeper _timeKeeper;
    private bool _flash = true;
    private float _beatDelay;
    private Renderer _rend;

    private void Start()
    {
        _beatDelay = 60 / Singleton.Global.Audio.CurrentChart.BPM;
        _rend = GetComponent<Renderer>();
        _timeKeeper = Singleton.Global.State.Player.GetComponent<TimeKeeper>();
    }

    private void Update()
    {
        if (_flash)
        {
            StartCoroutine(Flash(
                delay: _beatDelay,
                flashTime: flashTime,
                noteDetected: Singleton.Global.Audio.CurrentChart.CheckTolerance(_timeKeeper.TimeUnpaused, out _, out _)));
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