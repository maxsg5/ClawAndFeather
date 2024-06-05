using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/Global/Song Parser")]
public class SongParser : MonoBehaviour
{
    public TextAsset[] filesToParse;

    /// <summary>
    /// When giving a <paramref name="file"/> make sure it is a csv split into rows and follows this pattern:
    /// <br />BPM,EarlyTolerance,LateTolerance
    /// <br />float,float,float
    /// <br />NumberOfBeats,Notes/Beats
    /// <br />int,int/int
    /// <br />int,int/int
    /// <br />...
    /// </summary>
    /// <returns><see langword="true"/> or <see langword="false"/> based on if it was able to read the file or not.</returns>
    public static bool ReadCSVFile(TextAsset file, out SongChart chart)
    {
        chart = null; // what it returns if it returns false
        string[] dataLines = file.text.Split('\n'); // split by each new line
        string[] nonRepeatingData = dataLines[1].Split(',');
        if (nonRepeatingData.Length != 3)
        {
            Debug.LogError("Length of BPM,ToleranceEarly,ToleranceLate does not meet format.");
            return false;
        }

        #region Checking the Non Repeating Data
        if (!float.TryParse(nonRepeatingData[0], out float bpm)
          || !float.TryParse(nonRepeatingData[1], out float earlyTolerance)
          || !float.TryParse(nonRepeatingData[2], out float lateTolerance))
        {
            Debug.LogError("Error with BPM, ToleranceEarly, or ToleranceLate.");
            return false;
        }
        #endregion

        List<Note> notes = new();
        float totalBeatTime = 0;

        for (int i = 3; i < dataLines.Length; i++)
        {
            try
            {
                string[] row = dataLines[i].Split(',');
                
                if (row.Length != 2)
                { throw new Exception($"Row {i} of file {file.name} is in the incorrect format."); }
                
                int numberOfNotes = int.Parse(row[0]);
                string[] timeSignatureString = row[1].Split("/");
                float timeSignature = int.Parse(timeSignatureString[0]) / float.Parse(timeSignatureString[1]);
                float beatDelay = 0;

                // notes/beat * beats/minute = notes/minute
                // notes/minute * minutes/second = notes/second
                // notes/second ^-1 = seconds/note = beatDelay

                if (numberOfNotes == 0)
                {
                    beatDelay = 1 / (timeSignature * bpm / 60f);
                    notes.Add(new Note(totalBeatTime, true));
                    totalBeatTime += beatDelay;
                }
                else
                {
                    for (int j = 0; j < numberOfNotes; j++)
                    {
                        beatDelay = 1 / (timeSignature * bpm / 60f);
                        notes.Add(new Note(totalBeatTime, false));
                        totalBeatTime += beatDelay;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }
        }

        chart = new SongChart(file.name, bpm, -earlyTolerance, lateTolerance, notes.ToArray());
        return true;
    }
}
