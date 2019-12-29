using Game.JobSystem;
using Game.Managers;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject partsMarkerPrefab;
    [SerializeField] private GameObject progressMarkerPrefab;

    [Header("Parameters")]
    [SerializeField] private float constructionTime = 5f;
    [SerializeField] private Material activeMaterial;

    private GameObject partsMarker;
    private GameObject progressMarker;
    private bool hasParts;
    private bool isFinished;

    public void OnSpawn() {
        /*
         * Called by OrderController when device is bought and its blueprint is 
         * spawned on the scene. 
         */
        displayMissingPartsMarker();
    }

    public void OnPartsDelivered()
    {
        hasParts = true;
        Destroy(partsMarker);

        var job = new AssembleDeviceJob(gameObject);
        JobManager.Instance.ScheduleJob(job);
    }

    public void StartConstruction()
    {
        displayProgressMarker();
    }

    public bool IsFinished() { return isFinished; }

    private void displayMissingPartsMarker()
    {
        var canvas = GameStateManager.Instance.MarkerCanvas.transform;
        partsMarker = Instantiate(partsMarkerPrefab, canvas);
        Marker marker = partsMarker.GetComponent<Marker>();
        marker.SetTarget(transform);
    }

    private void displayProgressMarker()
    {
        var canvas = GameStateManager.Instance.MarkerCanvas.transform;
        progressMarker = Instantiate(progressMarkerPrefab, canvas);

        var progress = progressMarker.GetComponent<ProgressBar>();
        progress.Populate(transform, 5f, finishConstruction);
        progress.Trigger();
    }

    private void finishConstruction()
    {
        isFinished = true;
        var renderer = GetComponent<Renderer>();
        renderer.material = activeMaterial;
    }

    private void Awake()
    {
        hasParts = false;
        isFinished = false;
    }

}
