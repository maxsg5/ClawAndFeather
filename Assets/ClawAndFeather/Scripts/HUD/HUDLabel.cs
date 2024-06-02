using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public abstract class HUDLabel : MonoBehaviour
{
    private TextMeshProUGUI _label;
    private string _defaultText;
    protected GameState _state;

    // Start is called before the first frame update
    void Start()
    {
        _state = Singleton.Global.State;
        _label = GetComponent<TextMeshProUGUI>();
        _defaultText = _label.text.Trim();
    }

    protected void SetLabelText(string text) => _label.text = _defaultText + " " + text;
}
