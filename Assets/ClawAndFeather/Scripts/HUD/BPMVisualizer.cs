using System.Collections;
using UnityEngine;

public class BPMVisualizer : MonoBehaviour
{
    #region Inspector
    public Color onColour = Color.white;
    public Color offColour = Color.black;
    public float flashTime = 0.1f;
    #endregion

    private Renderer _rend;
    private float _bps;
    private bool _flashing = false;

    private void Start()
    {
        _rend = GetComponent<Renderer>();
    }

    void Update()
    {
        _bps = Singleton.Global.Audio.CurrentChart.BPM / 60f;

        if (!_flashing)
        {
            StartCoroutine(Flash());
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Flash()
    {
        _rend.material.color = onColour;
        yield return new WaitForSeconds(flashTime);
        _rend.material.color = offColour;
    }

    private IEnumerator Wait()
    {
        _flashing = true;
        yield return new WaitForSeconds(1 / _bps);
        _flashing = false;
    }
}
