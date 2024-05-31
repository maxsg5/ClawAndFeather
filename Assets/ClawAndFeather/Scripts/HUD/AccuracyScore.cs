using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccuracyScore : HUDLabel
{
    private void Update()
    {
        SetLabelText($"{_state.Score}");
    }
}
