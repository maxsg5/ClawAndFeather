public class Lives : HUDLabel
{
    private void Update()
    {
        SetLabelText(Singleton.Global.State.Player.Lives.ToString());
    }
}
