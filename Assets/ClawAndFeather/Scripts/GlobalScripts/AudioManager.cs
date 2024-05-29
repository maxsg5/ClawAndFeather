using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private SongParser _parser;
    private List<SongChart> _songCharts = new List<SongChart>();
    private int _currentChartID = 0;

    public AudioSource[] Songs { get; private set; }
    
    void Awake()
    {
        _parser = GetComponent<SongParser>();
        for (int i = 0; i < _parser.filesToParse.Length; i++)
        {
            if (SongParser.ReadCSVFile(_parser.filesToParse[i], out SongChart chart))
            { _songCharts.Add(chart); }  
        }
        Songs = GetComponents<AudioSource>();
    }

    /// <summary>
    /// Gets the current chart being used.
    /// </summary>
    public SongChart CurrentChart => _songCharts[_currentChartID];

    /// <summary>
    /// Gets the current time of the song currently being played
    /// </summary>
    public float AudioTime => Songs[_currentChartID].time;
}
