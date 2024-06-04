using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/HUD/Accuracy")]
public class AccuracyScore : HUDLabel
{
    private void Update()
    {
        SetLabelText($"{_state.Score}");
    }
}
