using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("General")]

    /* How fast camera is moving */
    [SerializeField] private float moveSpeed = 10f;

    /* Size of the border, when hovered camera starts moving */
    [SerializeField] private float edgeSize = 10f;

    [Header("Zoom")]
    [SerializeField] private float maxZoom = 3f;
    [SerializeField] private float minZoom = 10f;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        camera.enabled = true;
        camera.orthographicSize = (maxZoom + minZoom) / 2;
    }

    void Update()
    {
        Vector3 moveVector = getMoveVector() * moveSpeed * Time.deltaTime;

        transform.Translate(moveVector);
    }

    private Vector3 getMoveVector()
    {
        /**
         * Returns normalized move vector for camera. In case when cursor is 
         * not hitting the edge, zero vector is returned. 
         */
        Vector3 moveVector = new Vector3();

        // Vertical movement
        if (Input.mousePosition.y > Screen.height - edgeSize) { moveVector.y = 1; }
        else if (Input.mousePosition.y < edgeSize) { moveVector.y = -1; }

        // Horizontal movement
        if (Input.mousePosition.x > Screen.width - edgeSize) { moveVector.x = 1; }
        else if (Input.mousePosition.x < edgeSize) { moveVector.x = -1; }

        return moveVector.normalized;
    }
}
