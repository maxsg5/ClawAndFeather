using System.Linq;

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

    public static bool CheckTolerance(SongChart chart, float inputTime, out float accuracy)
    {
        accuracy = 0f;
        bool hitNote = false;

        Note previousNote = chart.Notes.Where(n => n.IsRest == false && n.BeatTime <= inputTime).FirstOrDefault();


        Note nextNote = chart.Notes.Where(n => n.IsRest == false && n.BeatTime >= inputTime).FirstOrDefault();



        return hitNote;
    }
}