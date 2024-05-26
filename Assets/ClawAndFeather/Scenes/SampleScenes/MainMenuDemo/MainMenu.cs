using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] menuButtons;
    private int _currentIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        HighlightedButton(_currentIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentIndex = (_currentIndex + 1) % menuButtons.Length;
            HighlightedButton(_currentIndex);
        }
    }

    void HighlightedButton(int index)
    {
        foreach (Button button in menuButtons)
        {
            var colors = button.colors;
            colors.normalColor = Color.red;
            button.colors = colors;
        }

        var selectedColors = menuButtons[index].colors;
        selectedColors.normalColor = Color.green;
        menuButtons[index].colors = selectedColors;
    }
}
