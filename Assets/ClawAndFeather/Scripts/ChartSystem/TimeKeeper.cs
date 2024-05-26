using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using System.Linq;

public class TimeKeeper : MonoBehaviour
{
    [field:SerializeField] public float TimeUnpaused { get; private set; }

    public List<float> AccuracyOfHitNotes { get; private set; }

    private void Update()
    {
        TimeUnpaused += Time.deltaTime;
    }

    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    Singleton.Global.Audio.GetCurrentChart().CheckTolerance(TimeUnpaused, out var accuracy);
                    AccuracyOfHitNotes.Add(accuracy);
                    Singleton.Global.State.SetScore(AccuracyOfHitNotes.Average());
                    break;
                case HoldInteraction: // Pause
                    break;
            }
        }
    }
}

public static class TemporaryExtensions // This class only exists to fix the compile errors.
{
    public static SongChart GetCurrentChart(this AudioManager manager)
    {
        throw new System.NotImplementedException();
    }

    public static void SetScore(this GameState gameState, float score)
    {
        throw new System.NotImplementedException();
    }
}