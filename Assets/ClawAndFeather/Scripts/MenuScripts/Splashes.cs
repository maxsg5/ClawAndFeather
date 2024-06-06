using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splashes : MonoBehaviour
{
    private enum Splash { None, Continue, Death, Win };
    private Splash _currentSplashActive;

    [Header("Continue Settings")]
    [SerializeField] private GameObject _continueSplash;
    [SerializeField] private GameObject _continueMenu;
    [SerializeField, Min(0)] private float _continueFadeInTime;
    void Start()
    {
        try
        {
            _continueSplash.SetActive(false);
            _continueMenu.SetActive(false);
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
}
