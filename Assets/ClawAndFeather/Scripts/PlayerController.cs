using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ButtonControl(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            switch (context.interaction)
            {
                case TapInteraction:
                    Debug.Log("Tapped");
                    break;
                case HoldInteraction:
                    Debug.Log("Held");
                    break;
            }
        }
    }
}
