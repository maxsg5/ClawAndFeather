using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    #region Movement
    private Rigidbody2D _body;

    private float _direction = 1; // 1 is right, -1 is left
    [field:SerializeField] public float Speed { get; set; } = 5f;
    
    #endregion

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        _body.AddForce(_direction * Vector2.right * Speed);

        if (_body.velocity.magnitude > Speed) 
        { _body.velocity = Vector2.ClampMagnitude(_body.velocity, Speed); }
    }
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction: // Reverse Direction
                    _direction *= -1;
                    break;
                case HoldInteraction: // Pause
                    break;
            }
        }
    }
}
