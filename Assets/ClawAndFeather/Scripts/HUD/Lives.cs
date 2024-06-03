using UnityEngine;

[AddComponentMenu("Scripts/HUD/Lives")]
public class Lives : HUDLabel
{
    private void Update()
    {
        SetLabelText(_state.Player.Lives.ToString());
    }
}
