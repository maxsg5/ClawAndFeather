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
}
