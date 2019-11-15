using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject missingPartsMarker;

    public void OnSpawn(GameObject markerCanvas) {
        /*
         * Called by OrderController when device is bought and its blueprint is 
         * spawned on the scene. 
         */
        GameObject markerObj = Instantiate(missingPartsMarker, markerCanvas.transform);
        Marker marker = markerObj.GetComponent<Marker>();
        marker.SetTarget(transform);
    }

}
