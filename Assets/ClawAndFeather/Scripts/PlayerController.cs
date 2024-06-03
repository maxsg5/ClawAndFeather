using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    #region Spinning
    [SerializeField] private Rigidbody2D _spinnerBody;
    [field: Space]
    #endregion

    #region Movement
    private Rigidbody2D _body;
    public int Direction { get; private set; } = 1; // 1 is right, -1 is left
    [field: SerializeField] public float Speed { get; set; } = 5f;
    [field: SerializeField] public float ImpulseMultiplier { get; set; } = 2f;
    [field: SerializeField] public float MaxVelocity { get; set; } = 10f;
    [field: Space]
    [field: SerializeField, Range(0, 9)] public int Lives { get; set; } = 9;

    #endregion

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Singleton.Global.Time.GameTime != 0)
        {
            _body.bodyType = RigidbodyType2D.Dynamic;
            _spinnerBody.bodyType = RigidbodyType2D.Dynamic;

            _body.AddForce(Direction * Speed * Vector2.right);
            _body.velocity = Vector2.ClampMagnitude(_body.velocity, MaxVelocity);
        }
        else
        {
            _body.bodyType = RigidbodyType2D.Static;
            _spinnerBody.bodyType = RigidbodyType2D.Static;
        }
    }
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed && Singleton.Global.Time.GameTime != 0)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    Direction *= -1;
                    _body.AddForce(Direction * Speed * ImpulseMultiplier * Vector2.right, ForceMode2D.Impulse);
                    break;
                case HoldInteraction: // Pause
                    Singleton.Global.State.Pause();
                    break;
            }
        }
    }
}
