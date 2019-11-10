using UnityEngine;

public class Utils
{
    public const int KEY_LMB = 0;
    public const int KEY_RMB = 1;

    public static Vector3 SnapToGrid(Vector3 position, float gridSize)
    {
        float newX = Mathf.Round(position.x / gridSize) * gridSize;
        float newZ = Mathf.Round(position.z / gridSize) * gridSize;

        return new Vector3(newX, position.y, newZ);
    }

}
