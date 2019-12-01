using UnityEngine;

public class PackageStoreManager : MonoBehaviour
{
    #region Editor Variables
    [Header("Platform parameters")]
    // How many packages can be placed
    [SerializeField] private int rowCap;
    [SerializeField] private int colCap;
    [SerializeField] private int heightCap;
    [SerializeField] private float packageSize = 0.7f;
    [SerializeField] private float packageInterspace = 0.1f;

    [Header("Package")]
    [SerializeField] private GameObject packagePrefab;
    #endregion

    #region Private Members
    private float platformSizeX;
    private float platformSizeZ;

    private GameObject[,,] platformSlots;
    #endregion

    #region Public Members
    public void PushPackage()
    {
        GameObject package = Instantiate(packagePrefab, gameObject.transform);
        package.name = string.Format("{0}_package", 1);

        try
        {
            Vector3Int freeSlot = getFreeSlot();
            placeAtSlot(package, freeSlot);
        }
        catch (UnityException err) { Debug.LogError(err.Message); }
    }

    public void PopPackage(Vector3Int slot)
    {
        Debug.Log("Popping...");
        if (!platformSlots[slot.x, slot.y, slot.z]) return;

        Destroy(platformSlots[slot.x, slot.y, slot.z]);

        for (int y = slot.y + 1; y < heightCap; y++)
        {
            // Break if there is nothing at current position
            if (!platformSlots[slot.x, y, slot.z]) break;

            // Move package one slot down
            placeAtSlot(platformSlots[slot.x, y, slot.z], 
                new Vector3Int(slot.x, y - 1, slot.z));
            platformSlots[slot.x, y, slot.z] = null;
        }
    }
    #endregion

    #region Private Methods
    private Vector3 getPositionFromSlot(Vector3Int slot)
    {
        /* Returns local position of package placed in given slot */
        float x = -((platformSizeX - packageSize) / 2) + 
            slot.x * (packageSize + packageInterspace);
        float z = -((platformSizeZ - packageSize) / 2) +
            slot.z * (packageSize + packageInterspace);
        float y = packageSize * (slot.y + 0.5f);

        return new Vector3(x, y, z);
    }
   
    private Vector3Int getFreeSlot()
    {
        for (int y = 0; y < heightCap; y++)
            for (int x = 0; x < rowCap; x++)
                for (int z = 0; z < colCap; z++)
                    if (!platformSlots[x, y, z]) return new Vector3Int(x, y, z);
        throw new UnityException("No free slots on platform");
    }
    
    private void placeAtSlot(GameObject package, Vector3Int slot)
    {
        platformSlots[slot.x, slot.y, slot.z] = package;
        package.transform.localPosition = getPositionFromSlot(slot);
    }
    #endregion

    #region Engine built-in
    private void Awake()
    {
        platformSizeX = rowCap * (packageSize + packageInterspace) - packageInterspace;
        platformSizeZ = colCap * (packageSize + packageInterspace) - packageInterspace;

        platformSlots = new GameObject[rowCap, colCap, heightCap];
    }
    #endregion
}
