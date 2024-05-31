public class Lives : HUDLabel
{
    private void Update()
    {
        SetLabelText(_state.Player.Lives.ToString());
    }
}
