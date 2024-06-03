using System;
using System.Collections;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private float _gameTimeScale = 1.0f;
    [SerializeField] private float _UITimeScale = 1.0f;
    public float GameTime { get; private set; }
    public float UITime { get; private set; }
    public void SetGameTime(float gameTime) => _gameTimeScale = gameTime;
    void Update()
    {
        GameTime = Time.deltaTime * _gameTimeScale;
        UITime = Time.deltaTime * _UITimeScale;
    }

    public enum Scope { Game, UI, Global }
    /// <summary>
    /// Use this method to set a variable followed by waiting, then setting the variable later to a different value. <br />
    /// Use ( result => [ your variable name here ] = result ) in place of the <paramref name="variable"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="variable"></param>
    /// <param name="time"></param>
    /// <param name="initialValue"></param>
    /// <param name="finalValue"></param>
    public IEnumerator DelayedGameVarChange<T>(Action<T> variable, float time, T initialValue, T finalValue)
    {
        variable(initialValue);
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += GameTime;
            yield return null;
        }
        variable(finalValue);
    }

    /// <summary>
    /// Invokes an <paramref name="action"/> after an amount of <paramref name="time"/> has passed.
    /// </summary>
    public IEnumerator DelayedAction(float time, Action action) => DelayedAction(time, action, Scope.Game);
    public IEnumerator DelayedAction(float time, Action action, Scope scope)
    {
        float currentTime = 0;
        while (currentTime < time)
        {
            currentTime += scope switch
            {
                Scope.UI => UITime,
                Scope.Game => GameTime,
                _ => Time.deltaTime,
            };
            yield return null;
        }
        action();
    }
}
