using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private SongParser _parser;
    private List<SongChart> _songCharts = new List<SongChart>();
    private int _currentChartID = 0;

    private float _currentVolume = 1.0f;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); // set currently setted volume
    }

    /// <summary>
    /// Gets the current chart being used.
    /// </summary>
    public SongChart CurrentChart => _songCharts[_currentChartID];

    /// <summary>
    /// Gets the current time of the song currently being played
    /// </summary>
    public float AudioTime => Songs[_currentChartID].time;

    /// <summary>
    /// Change the volume of the camera listener. There is a minimum value of 0 and maximum of 1. <br />
    /// The <paramref name="volumeModifier"/> is the modifier to the existing volume.
    /// </summary>
    /// <param name="volumeModifier"></param>
    public void ChangeVolume(float volumeModifier)  
    {
        _currentVolume = Mathf.Clamp01(_currentVolume + volumeModifier);
        PlayerPrefs.SetFloat("Volume", _currentVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); // set currently setted volume
    }
}
