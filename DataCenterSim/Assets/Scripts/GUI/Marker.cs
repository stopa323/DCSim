using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject tooltip;

    private Camera camera;

    public void ToggleTooltip(bool enabled) { tooltip.active = enabled; }

    public void SetTarget(Transform target) { this.target = target; }

    private void Awake()
    {
        camera = Camera.main;
    }

    void LateUpdate()
    {
        if (target) img.transform.position = camera.WorldToScreenPoint(target.position);
    }
}
