using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;
    [SerializeField] private GameObject[] _buttonObjects;
    private Button[] _buttons;
    private int _selectedButton;

    void Awake()
    {
        _buttons = new Button[_buttonObjects.Length];
        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttons[i] = _buttonObjects[i].GetComponent<Button>();
        }
        _selectedButton = 0;
        //_buttons[0].onClick.AddListener(() => ChangeScene(1)); //play button
    }
    private void ChangeScene(int ID) => SceneManager.LoadScene(ID);
    public void MenuControls(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction: // go to next
                    _selectedButton = (_selectedButton + 1) % _buttonObjects.Length;
                    _eventSystem.SetSelectedGameObject(_buttonObjects[_selectedButton]);
                    break;
                case HoldInteraction: // click button
                    _buttons[_selectedButton].onClick.Invoke();
                    StartCoroutine(Flash(_buttonObjects[_selectedButton].GetComponent<Image>(), 1));
                    break;
            }
        }
    }
    private IEnumerator Flash(Image image, int duration)
    {
        float timePassed = 0;
        Color originalColor = image.color;
        while (timePassed < duration * 0.5f)
        {
            timePassed += Time.deltaTime;
            image.color = Color.Lerp(image.color, Color.red, 0.1f);
            yield return null;
        }
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            image.color = Color.Lerp(image.color, Color.white, 0.1f);
            yield return null;
        }
    }
}
