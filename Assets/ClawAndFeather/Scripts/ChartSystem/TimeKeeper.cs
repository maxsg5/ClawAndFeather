using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using System.Linq;

public class TimeKeeper : MonoBehaviour
{
    public float TimeUnpaused => Singleton.Global.State.TimeUnpaused;

    public List<float> AccuracyOfHitNotes { get; private set; }

    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    Singleton.Global.Audio.GetCurrentChart().CheckTolerance(TimeUnpaused, out var accuracy);
                    AccuracyOfHitNotes.Add(accuracy);
                    Singleton.Global.State.Score = AccuracyOfHitNotes.Average();
                    break;
                case HoldInteraction: // Pause
                    break;
            }
        }
    }
}