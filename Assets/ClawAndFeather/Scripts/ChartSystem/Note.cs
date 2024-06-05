using UnityEngine;

public class Note
{
    public float NoteTime { get; private set; }
    public bool IsRest { get; private set; }
    public bool WasPlayed { get; private set; }

    public Note(float beatTime, bool isRest)
    {
        if (beatTime < 0)
        {
            throw new System.ArgumentOutOfRangeException(nameof(beatTime));
        }

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
