using System;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/HUD/Altimeter")]
public class Altimeter : HUDLabel
{
    public float maxAltitude = 1000;
    [Min(0)] public int digits;
    public UnityEngine.UI.Slider progressBar;

    private void Update()
    {
        float progress = Singleton.Global.Audio.SongProgress;
        SetLabelText($"{Math.Round(progress * maxAltitude, digits)}m");

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
