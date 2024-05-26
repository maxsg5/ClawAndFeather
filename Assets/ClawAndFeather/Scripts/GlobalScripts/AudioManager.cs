using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private SongParser _parser;
    private List<SongChart> _songCharts = new List<SongChart>();
    void Awake()
    {
        _parser = GetComponent<SongParser>();
        for (int i = 0; i < _parser.filesToParse.Length; i++)
        {
            if (SongParser.ReadCSVFile(_parser.filesToParse[i], out SongChart chart))
            { _songCharts.Add(chart); }  
        }
    }
    void Update()
    {
        
    }
}
