using UnityEngine;

[AddComponentMenu("Scripts/Claw and Feather/HUD/Lives")]
public class Lives : HUDLabel
{
    private void Update()
    {
        SetLabelText(_state.Player.Lives.ToString());
    }
}
