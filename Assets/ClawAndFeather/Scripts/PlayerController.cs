using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    #region Movement
    private float _direction = 1; // 1 is right, -1 is left
    public float Speed { get; set; } = 5f;
    #endregion

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(_direction * Vector2.right * Speed * Time.deltaTime);
    }
    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction:
                    _direction *= -1;
                    Debug.Log("Tapped");
                    break;
                case HoldInteraction:
                    Debug.Log("Held");
                    break;
            }
        }
    }
}
