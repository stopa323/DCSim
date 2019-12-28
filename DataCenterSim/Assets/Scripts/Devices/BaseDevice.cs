using Game.JobSystem;
using UnityEngine;

public class BaseDevice : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject partsMarkerPrefab;

    [Header("Parameters")]
    [SerializeField] private float constructionTime = 5f;
    [SerializeField] private Material activeMaterial;

    private enum State { Init, Constructing, Active};
    private GameObject partsMarker;
    private State state;
    private bool hasParts;
    private float constructionTimer;

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

        var job = new AssembleDeviceJob(gameObject);
        JobManager.Instance.ScheduleJob(job);
    }

    public void StartConstruction()
    {
        state = State.Constructing;
        constructionTimer = 0f;
    }

    public bool IsFinished() { return State.Active == state; }

    private void Awake()
    {
        hasParts = false;
        state = State.Init;
    }

    private void Update()
    {
        switch (state)
        {
            case State.Constructing:
                constructionTimer += Time.deltaTime;
                if (constructionTimer >= constructionTime)
                {
                    state = State.Active;
                    var renderer = GetComponent<MeshRenderer>();
                    renderer.material = activeMaterial;
                }
                break;
            default:
                break;
        }
    }
}
