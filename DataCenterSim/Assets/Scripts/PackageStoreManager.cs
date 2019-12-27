using Game.JobSystem;
using Game.Structures;
using System.Collections.Generic;
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

    // Separated in order to easily dequeue (tail always)
    private List<Package> packages;
    private List<GameObject> packageMeshes;
    #endregion

    #region Public Members
    public void PushPackage(Package package)
    {
        // Check if there is place on the store
        if (packages.Count == packages.Capacity)
            throw new UnityException("No free space in the store");

        // Update Package with object and place to store
        GameObject package_mesh = Instantiate(packagePrefab, gameObject.transform);
        package_mesh.name = string.Format("{0}_package", 1);

        place(package, package_mesh);

        var job = new DeliverPackageJob(package);
        JobManager.Instance.ScheduleJob(job);
    }

    public void PopPackage(Package package)
    {
        packages.Remove(package);

        var lastIndex = packageMeshes.Count - 1;
        var mesh = packageMeshes[lastIndex];

        Destroy(mesh);
        packageMeshes.RemoveAt(lastIndex);
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
        int x = packages.Count % rowCap;
        int z = packages.Count / colCap % rowCap;
        int y = packages.Count / (rowCap * colCap);

        return new Vector3Int(x, y, z);
    }
    
    // TODO: Is Package ref necessary here? Is is already saved in Job Object and
    //       we dont need mesh-to-package link as for now.
    private void place(Package package, GameObject mesh)
    {
        packageMeshes.Add(mesh);
        mesh.transform.localPosition = getPositionFromSlot(getFreeSlot());

        // Must be called after getFreeSlot, as the method bases on package count
        packages.Add(package);
        package.Store = this;
    }
    #endregion

    #region Engine built-in
    private void Awake()
    {
        platformSizeX = rowCap * (packageSize + packageInterspace) - packageInterspace;
        platformSizeZ = colCap * (packageSize + packageInterspace) - packageInterspace;

        packages = new List<Package>(rowCap * colCap * heightCap);
        packageMeshes = new List<GameObject>(rowCap * colCap * heightCap);
    }
    #endregion
}
