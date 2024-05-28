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
        if (toleranceEarly >= 0)
        {
            throw new ArgumentException(nameof(toleranceEarly));
        }
        if (toleranceLate <= 0)
        {
            throw new ArgumentException(nameof(toleranceLate));
        }
        if (bpm <= 0)
        {
            throw new ArgumentException(nameof(bpm));
        }

        BPM = bpm;
        ToleranceEarly = toleranceEarly;
        ToleranceLate = toleranceLate;
        Notes = notes;
    }

    public bool CheckTolerance(float inputTime, out Note hitNote, out float accuracy) => CheckTolerance(inputTime, out hitNote, out accuracy, out _);
    public bool CheckTolerance(float inputTime, out Note hitNote, out float accuracy, out float timeDiff)
    {
        Note checkNote = GetNearestNote(inputTime);

        timeDiff = inputTime - checkNote.BeatTime;

        accuracy = timeDiff < 0
            ? ToleranceEarly / timeDiff
            : ToleranceLate / timeDiff;

        bool noteHit = ToleranceEarly <= timeDiff & timeDiff <= ToleranceLate;

        hitNote = noteHit ? checkNote : null;

        return noteHit;
    }

    private Tuple<Note, Note> GetNearestNotePair(float inputTime)
    {
        Note previousNote = Notes.Where(n => n.IsRest == false & n.BeatTime <= inputTime).FirstOrDefault();
        Note nextNote = Notes.Where(n => n.IsRest == false & n.BeatTime >= inputTime).FirstOrDefault();

        return Tuple.Create(previousNote, nextNote);
    }

    public Note GetNearestNote(float time)
    {
        Tuple<Note, Note> checkNotes = GetNearestNotePair(time);

        var prevNote = checkNotes.Item1;
        var nextNote = checkNotes.Item2;

        float nextTimeDiff = Math.Abs(time - nextNote.BeatTime);
        float prevTimeDiff = Math.Abs(time - prevNote.BeatTime);

        return (prevTimeDiff >= nextTimeDiff) ? prevNote : nextNote;
    }
}