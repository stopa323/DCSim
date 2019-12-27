using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject partsMarkerPrefab;

    private GameObject partsMarker;

    private bool hasParts;

    public void OnSpawn(GameObject markerCanvas) {
        /*
         * Called by OrderController when device is bought and its blueprint is 
         * spawned on the scene. 
         */
        partsMarker = Instantiate(partsMarkerPrefab, markerCanvas.transform);
        Marker marker = partsMarker.GetComponent<Marker>();
        marker.SetTarget(transform);
    }

    public void OnPartsDelivered()
    {
        hasParts = true;
        Destroy(partsMarker);
    }

    private void Awake()
    {
        hasParts = false;
    }
}
