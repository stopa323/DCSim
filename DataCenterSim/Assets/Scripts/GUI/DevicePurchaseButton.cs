using Game.Events;
using UnityEngine;

public class DevicePurchaseButton : BaseButton
{
    public GameObject DevicePrefab;
    public GameObjectEvent OnDeviceSelected;

    public override void OnCursorClick()
    {
        base.OnCursorClick();
        OnDeviceSelected.Raise(DevicePrefab);
    }
}
