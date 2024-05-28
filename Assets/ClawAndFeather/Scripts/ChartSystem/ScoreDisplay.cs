using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour
{
    public string preText;

    private TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        scoreLabel = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (scoreLabel != null)
        {
            scoreLabel.text = preText + Singleton.Global.State.Score;
        }
    }
}
