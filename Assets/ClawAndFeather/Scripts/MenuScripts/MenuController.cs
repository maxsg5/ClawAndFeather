using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public abstract class MenuController : MonoBehaviour
{
    [SerializeField] protected GameObject[] _buttonObjects;
    protected Button[] _buttons;
    protected Image[] _buttonImages;
    protected Animator[] _buttonAnimations;
    public int selectedButton;

    protected Coroutine _fadeCoroutine;
    protected Coroutine _flashCoroutine;
    [Space]
    public Color submittedButtonColor;
    public Color selectedButtonColor;
    public Color unselectedButtonColor;

    protected PlayerInput _playerInput;
    
    internal virtual void Awake()
    {
        _buttons = new Button[_buttonObjects.Length];
        _buttonImages = new Image[_buttonObjects.Length];
        _buttonAnimations = new Animator[_buttonObjects.Length];
        _playerInput = GetComponent<PlayerInput>();

        for (int i = 0; i < _buttonObjects.Length; i++)
        {
            _buttons[i] = _buttonObjects[i].GetComponent<Button>();
            _buttonImages[i] = _buttonObjects[i].GetComponent<Image>();
            _buttonAnimations[i] = _buttonObjects[i].GetComponent<Animator>();
        }
        selectedButton = 0;
    }
    internal void OnEnable()
    {
        _playerInput.ActivateInput();
        StartCoroutine(Initialize(1.5f));
    }
    internal IEnumerator Initialize(float waitTime)
    {   
        for (int i = 0; i < _buttonObjects.Length; i++)
        { _buttonAnimations[i].SetBool("Selected", false); }
        _buttonImages[selectedButton].color = unselectedButtonColor;
        yield return new WaitForSecondsRealtime(waitTime);
        _buttonImages[selectedButton].color = selectedButtonColor;
        _buttonAnimations[selectedButton].SetBool("Selected", true);
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

                    _flashCoroutine = StartCoroutine(Flash(_buttonImages[selectedButton], 1));
                    _buttons[selectedButton].onClick.Invoke();
                    
                    break;
            }
        }
    }
    internal IEnumerator Fade(Image image, float fadeDuration, float fadeLerp)
    {
        float timePassed = 0;
        while (timePassed < fadeDuration)
        {
            timePassed += Time.unscaledDeltaTime;
            image.color = Color.Lerp(image.color, selectedButtonColor, fadeLerp);
            yield return null;
        }
    }
    internal IEnumerator Flash(Image image, int duration)
    {
        float timePassed = 0;
        while (timePassed < duration * 0.5f)
        {
            timePassed += Time.unscaledDeltaTime;
            image.color = Color.Lerp(image.color, submittedButtonColor, 0.1f);
            yield return null;
        }
        while (timePassed < duration)
        {
            timePassed += Time.unscaledDeltaTime;
            image.color = Color.Lerp(image.color, selectedButtonColor, 0.1f);
            yield return null;
        }
        image.color = selectedButtonColor;
    }
    public IEnumerator Outro(float waitTime, GameObject newActive)
    {
        for (int i = 0; i < _buttonObjects.Length; i++)
        { _buttonAnimations[i].SetTrigger("Leave"); }
        yield return new WaitForSecondsRealtime(waitTime);
        gameObject.SetActive(false);
        newActive.SetActive(true);
    }
}
