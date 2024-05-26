using System;
using System.Collections.Generic;
using UnityEngine;

public class SongParser : MonoBehaviour
{
    public TextAsset[] filesToParse;

    public static bool ReadCSVFile(TextAsset file, out SongChart chart)
    {
        chart = null;
        string[] dataLines = file.text.Split('\n'); // split by each new line
        string[] nonRepeatingData = dataLines[0].Split(',');
        if (nonRepeatingData.Length != 3)
        {
            Debug.LogError("Length of BPM,ToleranceEarly,ToleranceLate does not meet format.");
            
            return false;
        }

        #region Checking the Non Repeating Data
        float bpm = 0;
        float earlyTolerance = 0;
        float lateTolerance = 0;
        if ( !float.TryParse(nonRepeatingData[0], out bpm)
          || !float.TryParse(nonRepeatingData[1], out earlyTolerance)
          || !float.TryParse(nonRepeatingData[2], out lateTolerance))
        {
            Debug.LogError("Error with BPM, ToleranceEarly, or ToleranceLate.");
            return false;
        }
        #endregion
        
        List<Note> notes = new List<Note>();
        float totalBeatTime = 0;

        for (int i = 1; i < dataLines.Length; i++)
        {
            try
            {
                string[] row = dataLines[i].Split(',');
                
                if (row.Length != 2)
                { throw new Exception($"Row {i} of file {file.name} is in the incorrect format."); }
                
                int numberOfBeats = int.Parse(row[0]);
                string[] timeSignatureString = row[1].Split("/");
                float timeSignature = int.Parse(timeSignatureString[0]) / float.Parse(timeSignatureString[1]);
                
                float beatDelay = 0;
                if (numberOfBeats == 0)
                { beatDelay = beatDelay = 60 * timeSignature / bpm; }
                else
                { beatDelay = beatDelay = 60 * timeSignature / (numberOfBeats * bpm); }

                notes.Add(new Note(beatDelay, totalBeatTime, numberOfBeats == 0 ? true : false ));
                totalBeatTime += beatDelay;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }

        chart = new SongChart(bpm, earlyTolerance, lateTolerance, notes.ToArray());
        return true;
    }
}
