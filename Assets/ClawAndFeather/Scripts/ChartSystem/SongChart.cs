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

    // check whether the input time is close enough to the noteTime
    public bool TryPlayNote(float inputTime, out Note playedNote, out float accuracy) => TryPlayNote(inputTime, out playedNote, out accuracy, out _);
    public bool TryPlayNote(float inputTime, out Note playedNote, out float accuracy, out float timeDiff)
    {
        // Find nearest 2 notes
        Note nextNote = Notes.Where(n => !n.IsRest & (inputTime - n.NoteTime) >= 0).FirstOrDefault();
        Note prevNote = Notes.Where(n => !n.IsRest & (inputTime - n.NoteTime) <= 0).FirstOrDefault();

        float nextNoteDiff = inputTime - nextNote.NoteTime;
        float prevNoteDiff = inputTime - prevNote.NoteTime;

        // Pick the one closest to inputTime
        float checkDiff;
        if (Math.Abs(nextNoteDiff) >= Math.Abs(prevNoteDiff))
        {
            playedNote = prevNote;
            checkDiff = prevNoteDiff;

            accuracy = checkDiff / ToleranceLate;
        }
        else
        {
            playedNote = nextNote;
            checkDiff = nextNoteDiff;

            accuracy = checkDiff / ToleranceEarly;
        }

        timeDiff = checkDiff;
        // return whether or not the time is tolerated
        return ToleranceEarly < checkDiff & checkDiff < ToleranceLate;
    }

}