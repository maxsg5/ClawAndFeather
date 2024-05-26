using System;
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
        Note[] notes = chart.Notes;

        Note previousNote = notes.Where(n => n.IsRest == false & n.BeatTime <= inputTime).FirstOrDefault();
        Note nextNote = notes.Where(n => n.IsRest == false & n.BeatTime >= inputTime).FirstOrDefault();

        float nextTimeDiff = inputTime - nextNote.BeatTime;
        float prevTimeDiff = inputTime - previousNote.BeatTime;

        Note checkNote = nextNote.WasPlayed 
            ? previousNote 
            : (previousNote.WasPlayed 
                ? nextNote
                : (nextTimeDiff < prevTimeDiff
                    ? nextNote
                    : previousNote));
        
        float timeDiff = inputTime - checkNote.BeatTime;
        bool hitNote = chart.ToleranceEarly <= timeDiff & timeDiff <= chart.ToleranceLate;

        return hitNote;
    }
}