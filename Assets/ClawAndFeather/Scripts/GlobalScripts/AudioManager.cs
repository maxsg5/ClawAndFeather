using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private SongParser _parser;
    private List<SongChart> _songCharts = new List<SongChart>();
    private int _currentChartID = 0;

    private float _currentVolume = 1.0f;
    public float CurrentVolume { get => _currentVolume; private set => _currentVolume = value; }

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
    /// Returns <see cref="AudioTime"/> as a percentage of the <see cref="Note.NoteTime"/> of the final <see cref="Note"/> in <see cref="CurrentChart"/>.
    /// </summary>
    public float SongProgress => AudioTime / CurrentChart.Notes.Last().NoteTime;

    /// <summary>
    /// Change the volume of the camera listener. There is a minimum value of 0 and maximum of 1. <br />
    /// The <paramref name="volumeModifier"/> is the modifier to the existing volume.
    /// </summary>
    /// <param name="volumeModifier"></param>
    public void ChangeVolume(float volumeModifier)  
    {
        CurrentVolume = Mathf.Clamp01(CurrentVolume + volumeModifier);
        PlayerPrefs.SetFloat("Volume", CurrentVolume);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume"); // set currently setted volume
    }

    // TimeKeeper
    public List<float> HitNotes { get; private set; } = new();

    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    if (Singleton.Global.Audio.CurrentChart.TryPlayNote(AudioTime, out var hitNote, out var accuracy))
                    {
                        hitNote.Play();
                    }
                    HitNotes.Add(accuracy);
                    Singleton.Global.State.Score = HitNotes.Average();
                    break;
                case HoldInteraction: // Pause
                    break;
            }
        }
    }
}
