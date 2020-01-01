using Game.Managers;
using UnityEngine;

public class StaticMarker : MonoBehaviour
{
    // Reference to tooltip object
    [SerializeField] protected GameObject tooltip;

    protected Transform target;

    public void Populate(Transform target)
    {
        this.target = target;
    }

    public void ToggleTooltip(bool active)
    {
        tooltip.active = active;
    }

    protected void LateUpdate()
    {
        if (target)
        {
            // Adjust HUD position (billboard)
            var camera = GameStateManager.Instance.MainCamera;
            transform.position = camera.WorldToScreenPoint(target.position);
        }
    }
}
