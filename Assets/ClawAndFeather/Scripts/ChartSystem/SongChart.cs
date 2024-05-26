public class SongChart
{
    public float BPM { get; private set; }
    public float ToleranceEarly { get; private set; }
    public float ToleranceLate { get; private set; }

    public Note[] Notes { get; private set; }

    public SongChart(float bpm, float toleranceEarly, float toleranceLate, Note[] notes)
    {
        BPM = bpm;
        ToleranceEarly = toleranceEarly;
        ToleranceLate = toleranceLate;
        Notes = notes;
    }

    public bool CheckTolerance(float inputTime, out float accuracy)
    {
        accuracy = 0f;


    }
}