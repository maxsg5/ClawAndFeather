using System;
using System.Linq;

public class SongChart
{
    public float BPM { get; private set; }
    public float ToleranceEarly { get; private set; }
    public float ToleranceLate { get; private set; }

    public Note[] Notes { get; private set; }

    public Note[] UnplayedNotes => Notes.Where(n => !n.WasPlayed).ToArray();

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
        Notes = notes.OrderBy(n => n.NoteTime).ToArray();
    }

    // check whether the input time is close enough to the noteTime
    public bool TryPlayNote(float inputTime, out Note playedNote, out float accuracy) => TryPlayNote(inputTime, out playedNote, out accuracy, out _);
    public bool TryPlayNote(float inputTime, out Note playedNote, out float accuracy, out float timeDiff)
    {
        playedNote = UnplayedNotes.OrderBy(n => Math.Abs(inputTime - n.NoteTime)).FirstOrDefault();

        timeDiff = inputTime - playedNote.NoteTime;

        if (timeDiff > 0)
        {
            accuracy = Math.Abs(ToleranceLate / timeDiff);
        }
        else if (timeDiff < 0)
        {
            accuracy = Math.Abs(ToleranceEarly / timeDiff);
        }
        else
        {
            accuracy = 1;
        }

        return ToleranceEarly < timeDiff & timeDiff < ToleranceLate;
    }
}