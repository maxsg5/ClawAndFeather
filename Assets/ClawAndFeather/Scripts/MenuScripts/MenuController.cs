using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttonObjects;
    private Button[] _buttons;
    private Image[] _buttonImages;
    private Animator[] _buttonAnimations;
    public int selectedButton;

    private Coroutine _fadeCoroutine;
    private Coroutine _flashCoroutine;
    [Space]
    public Color submittedButtonColor;
    public Color selectedButtonColor;
    public Color unselectedButtonColor;
    
    void Awake()
    {
        _buttons = new Button[_buttonObjects.Length];
        _buttonImages = new Image[_buttonObjects.Length];
        _buttonAnimations = new Animator[_buttonObjects.Length];

        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttons[i] = _buttonObjects[i].GetComponent<Button>();
            _buttonImages[i] = _buttonObjects[i].GetComponent<Image>();
            _buttonAnimations[i] = _buttonObjects[i].GetComponent<Animator>();
        }
        selectedButton = 0;
        _buttons[0].onClick.AddListener(() => GameState.ChangeScene(1)); //play button
        _buttons[_buttonObjects.Length - 1].onClick.AddListener(GameState.ExitGame); // exit game
    }
    private void OnEnable()
    {
        StartCoroutine(Initialize(1));
    }
    private IEnumerator Initialize(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        _buttonImages[0].color = selectedButtonColor;
        _buttonAnimations[0].SetBool("Selected", true);
    }    
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

                    _buttonImages[selectedButton].color = unselectedButtonColor;
                    _buttonAnimations[selectedButton].SetBool("Selected", false);
                    selectedButton = (selectedButton + 1) % _buttonObjects.Length;
                    _buttonAnimations[selectedButton].SetBool("Selected", true);
                    _fadeCoroutine = StartCoroutine(Fade(_buttonImages[selectedButton], 1, 0.2f));

                    break;
                case HoldInteraction: // click button

                    _buttons[selectedButton].onClick.Invoke();
                    _flashCoroutine = StartCoroutine(Flash(_buttonImages[selectedButton], 1));

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
