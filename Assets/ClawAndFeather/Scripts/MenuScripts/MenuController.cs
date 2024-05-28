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
    private Image[] _buttonImages;
    private int _selectedButton;

    private Coroutine _fadeCoroutine;
    private Coroutine _flashCoroutine;

    public Color submittedButtonColor;
    public Color selectedButtonColor;
    void Awake()
    {
        _buttons = new Button[_buttonObjects.Length];
        _buttonImages = new Image[_buttonObjects.Length];
        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttons[i] = _buttonObjects[i].GetComponent<Button>();
            _buttonImages[i] = _buttonObjects[i].GetComponent<Image>();
        }
        _selectedButton = 0;
        _fadeCoroutine = StartCoroutine(Fade(_buttonImages[_selectedButton], 1, 0.2f));
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

                    if (_fadeCoroutine != null)
                    { StopCoroutine(_fadeCoroutine); }

                    if (_flashCoroutine != null)
                    { StopCoroutine(_flashCoroutine); }

                    _buttonImages[_selectedButton].color = Color.white;

                    _selectedButton = (_selectedButton + 1) % _buttonObjects.Length;

                    _fadeCoroutine = StartCoroutine(Fade(_buttonImages[_selectedButton], 1, 0.2f));
                    //_eventSystem.SetSelectedGameObject(_buttonObjects[_selectedButton]);

                    break;
                case HoldInteraction: // click button

                    _buttons[_selectedButton].onClick.Invoke();
                    _flashCoroutine = StartCoroutine(Flash(_buttonImages[_selectedButton], 1));

                    break;
            }
        }
    }
    private IEnumerator Fade(Image image, float fadeDuration, float fadeLerp)
    {
        float timePassed = 0;
        while (timePassed < fadeDuration)
        {
            timePassed += Time.deltaTime;
            image.color = Color.Lerp(image.color, selectedButtonColor, fadeLerp);
            yield return null;
        }
    }
    private IEnumerator Flash(Image image, int duration)
    {
        float timePassed = 0;
        while (timePassed < duration * 0.5f)
        {
            timePassed += Time.deltaTime;
            image.color = Color.Lerp(image.color, submittedButtonColor, 0.1f);
            yield return null;
        }
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            image.color = Color.Lerp(image.color, selectedButtonColor, 0.1f);
            yield return null;
        }
        image.color = selectedButtonColor;
    }
}
