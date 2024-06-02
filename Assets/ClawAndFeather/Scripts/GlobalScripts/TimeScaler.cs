using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    [SerializeField] private float _gameTimeScale = 1.0f;
    [SerializeField] private float _UITimeScale = 1.0f;
    public float GameTime => Time.deltaTime * _gameTimeScale;
    public float UITime => Time.deltaTime * _UITimeScale;
    public void SetGameTime(float gameTime) => _gameTimeScale = gameTime;
}
