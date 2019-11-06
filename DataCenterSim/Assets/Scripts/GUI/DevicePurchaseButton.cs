using Game.Events;

public class DevicePurchaseButton : BaseButton
{
    public VoidEvent E_Test;

    public override void OnCursorClick()
    {
        base.OnCursorClick();
        E_Test.Raise();
    }
}
