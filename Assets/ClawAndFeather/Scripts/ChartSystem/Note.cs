public struct Note
{
    public float BeatDelay { get; private set; }
    public float BeatTime { get; private set; }
    public bool IsRest { get; private set; }
    public bool WasPlayed { get; private set; }

    public Note(float beatDelay, float beatTime, bool isRest, bool wasPlayed)
    {
        BeatDelay = beatDelay;
        BeatTime = beatTime;
        IsRest = isRest;
        WasPlayed = wasPlayed;
    }
}
