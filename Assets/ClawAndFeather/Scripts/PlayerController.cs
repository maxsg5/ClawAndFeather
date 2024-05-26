using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    #region Movement
    private float _verticalDir = 0;
    [SerializeField] private float _angleMult = 10;
    private float _direction = 1; // 1 is right, -1 is left
    public float Speed { get; set; } = 5f;
    public Vector2 Velocity { get; set; }
    
    #endregion

    void Start()
    {
        
    }
    void Update()
    {
        _verticalDir = (Time.deltaTime * _angleMult + _verticalDir) % 360;
        Velocity = new Vector2(_direction, Mathf.Sin(_verticalDir)) * Speed;
        transform.Translate(Velocity * Time.deltaTime);
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
