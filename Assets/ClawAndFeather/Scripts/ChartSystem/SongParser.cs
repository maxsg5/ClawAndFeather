using UnityEngine;

public class SongChartParser : MonoBehaviour
{
    public TextAsset[] filesToParse;
    private string[] _dataLines;
    void Start()
    {
        for (int i = 0; i < filesToParse.Length; i++)
        {
            ReadCSVFile(filesToParse[i]);
        }
    }

    private void ReadCSVFile(TextAsset file)
    {
        _dataLines = file.text.Split('\n'); // split by each new line
        string[] nonRepeatingData = _dataLines[0].Split(',');
        if (nonRepeatingData.Length != 3)
        { 
            Debug.LogError("Length of BPM,ToleranceEarly,ToleranceLate does not meet format.");
            return;
        }

        #region Checking the Non Repeating Data
        int bpm = 0;
        float earlyTolerance = 0;
        float lateTolerance = 0;
        if ( !int.TryParse(nonRepeatingData[0], out bpm)
          || !float.TryParse(nonRepeatingData[1], out earlyTolerance)
          || !float.TryParse(nonRepeatingData[2], out lateTolerance))
        {
            Debug.LogError("Error with BPM, ToleranceEarly, or ToleranceLate.");
            return;
        }
        #endregion

        for (int i = 1; i < _dataLines.Length; i++)
        {
            string[] row = _dataLines[i].Split(',');
        }
    }
}
