using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Splashes : MonoBehaviour
{
    private enum Splash { None, Continue, GameOver, Win };
    private Splash _currentSplashActive;

    [Header("Continue Settings")]
    [SerializeField] private GameObject _continueSplash;
    [SerializeField] private GameObject _continueMenu;
    [SerializeField, Min(0)] private float _continueFadeInTime;

    [Header("GameOver Settings")]
    [SerializeField] private GameObject _gameOverSplash;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField, Min(0)] private float _gameOverFadeInTime;

    void Start()
    {
        try
        {
            _continueSplash.SetActive(false);
            _continueMenu.SetActive(false);

            _gameOverSplash.SetActive(false);
            _gameOverMenu.SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    void Update()
    {
        if (_currentSplashActive == Splash.None)
        {
            if (Singleton.Global.Audio.SongProgress > 1)
            {
                _currentSplashActive = Splash.Continue;
                StartCoroutine(ContinueSplash(_continueFadeInTime));
            }
            else if (Singleton.Global.State.Player.Lives <= 0)
            {
                _currentSplashActive = Splash.GameOver;
                StartCoroutine(GameOverSplash(_gameOverFadeInTime));
            }
        }
    }
    private IEnumerator ContinueSplash(float timeFadeIn)
    {
        Singleton.Global.State.LimitedPause(true);
        _continueSplash.SetActive(true);
        SpriteRenderer renderer = _continueSplash.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, 0);
        float currentTime = 0;
        while (currentTime < timeFadeIn)
        {
            currentTime += Time.unscaledDeltaTime;
            renderer.color = Color.Lerp(renderer.color, Color.white, 0.1f);
            yield return null;
        }
        renderer.color = Color.white;
        _continueMenu.SetActive(true);
    }

    private IEnumerator GameOverSplash(float timeFadeIn)
    {
        Singleton.Global.State.LimitedPause(true);
        Singleton.Global.Audio.PauseSong(true);
        _gameOverSplash.SetActive(true);
        SpriteRenderer renderer = _gameOverSplash.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1, 0);
        float currentTime = 0;
        while (currentTime < timeFadeIn)
        {
            currentTime += Time.unscaledDeltaTime;
            renderer.color = Color.Lerp(renderer.color, Color.white, 0.1f);
            yield return null;
        }
        renderer.color = Color.white;
        _gameOverMenu.SetActive(true);
    }
}
