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
    void FixedUpdate()
    {
        _body.AddForce(Direction * Speed * Vector2.right);
        _body.velocity = Vector2.ClampMagnitude(_body.velocity, MaxVelocity);
    }
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed && Time.timeScale != 0)
        {
            Debug.Log("Thing happened");
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
        else if (context.performed && Time.timeScale == 0)
        {
            Debug.Log("THing interrupted");
        }
    }
}
