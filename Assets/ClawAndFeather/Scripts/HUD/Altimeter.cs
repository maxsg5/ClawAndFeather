using UnityEngine;

public class Altimeter : HUDLabel
{
    public float maxAltitude = 1000;
    public UnityEngine.UI.Slider progressBar;

    private void Update()
    {
        float progress = Singleton.Global.Audio.SongProgress;
        SetLabelText($"{progress * maxAltitude}m");

        if (progressBar == null)
        {
            Debug.LogWarning($"No value was assigned for member {nameof(progress)} on {this.gameObject.name}.");
        }
        else
        {
            progressBar.value = progress;
        }
    }
}
