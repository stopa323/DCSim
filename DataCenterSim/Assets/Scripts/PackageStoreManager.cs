using Game.JobSystem;
using Game.Structures;
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

    private Package[,,] platformSlots;
    #endregion

    #region Public Members
    public void PushOrder(Order order)
    {
        foreach(Package package in order.Items)
        {
            try
            {
                // This will throw exception if no free slot is found
                Vector3Int freeSlot = getFreeSlot();

                // Update Package with object and place to store
                GameObject packageObj = Instantiate(packagePrefab, gameObject.transform);
                packageObj.name = string.Format("{0}_package", 1);
                package.AssignPackage(packageObj);

                placeAtSlot(package, freeSlot);

                // Add new job to the manager
                DeliverPartsJob job = new DeliverPartsJob(package.Object, package.OrderedItem);
                JobManager.Instance.ScheduleJob(job);
            }
            catch (UnityException err) {
                Debug.LogError(err.Message);
                return;
            }
        }
    }

    public void PopPackage(Vector3Int slot)
    {
        Debug.Log("Popping...");
        if (platformSlots[slot.x, slot.y, slot.z] == null) return;

        Destroy(platformSlots[slot.x, slot.y, slot.z].Object);

        for (int y = slot.y + 1; y < heightCap; y++)
        {
            // Break if there is nothing at current position
            if (platformSlots[slot.x, y, slot.z] == null) break;

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
                    if (platformSlots[x, y, z] == null) return new Vector3Int(x, y, z);
        throw new UnityException("No free slots on platform");
    }
    
    private void placeAtSlot(Package package, Vector3Int slot)
    {
        platformSlots[slot.x, slot.y, slot.z] = package;
        package.Object.transform.localPosition = getPositionFromSlot(slot);
    }
    #endregion

    #region Engine built-in
    private void Awake()
    {
        platformSizeX = rowCap * (packageSize + packageInterspace) - packageInterspace;
        platformSizeZ = colCap * (packageSize + packageInterspace) - packageInterspace;

        platformSlots = new Package[rowCap, colCap, heightCap];
    }
    #endregion
}
