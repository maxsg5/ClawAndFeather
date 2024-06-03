using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/HUD/Accuracy")]
public class AccuracyScore : HUDLabel
{
    private void Update()
    {
        SetLabelText($"{_state.Score}");
    }
}
