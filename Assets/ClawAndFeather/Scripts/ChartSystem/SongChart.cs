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

    public bool CheckTolerance(float inputTime, out float accuracy)
    {
        Note previousNote = Notes.Where(n => n.IsRest == false & n.BeatTime <= inputTime).FirstOrDefault();
        Note nextNote = Notes.Where(n => n.IsRest == false & n.BeatTime >= inputTime).FirstOrDefault();

        float nextTimeDiff = Math.Abs(inputTime - nextNote.BeatTime);
        float prevTimeDiff = Math.Abs(inputTime - previousNote.BeatTime);

        Note checkNote = nextNote.WasPlayed
            ? previousNote
            : (previousNote.WasPlayed
                ? nextNote
                : (nextTimeDiff < prevTimeDiff
                    ? nextNote
                    : previousNote));

        float timeDiff = inputTime - checkNote.BeatTime;

        accuracy = timeDiff < 0
            ? timeDiff / ToleranceEarly
            : timeDiff / ToleranceLate;

        bool hitNote = ToleranceEarly <= timeDiff & timeDiff <= ToleranceLate;

        return hitNote;
    }
}