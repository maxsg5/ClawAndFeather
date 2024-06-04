using UnityEngine;

public class Note
{
    public float BeatDelay { get; private set; }
    public float NoteTime { get; private set; }
    public bool IsRest { get; private set; }
    public bool WasPlayed { get; private set; }

    public Note(float beatDelay, float beatTime, bool isRest)
    {
        if (beatDelay <= 0)
        {
            throw new System.ArgumentOutOfRangeException(nameof(beatDelay));
        }
        if (beatTime < 0)
        {
            throw new System.ArgumentOutOfRangeException(nameof(beatTime));
        }

        BeatDelay = beatDelay;
        NoteTime = beatTime;
        IsRest = isRest;
        WasPlayed = false;
    }

    public void Play()
    {
        Debug.Log("A note was played");
        WasPlayed = true;
    }
}
