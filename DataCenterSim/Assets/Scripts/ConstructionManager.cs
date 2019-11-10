using UnityEngine;

namespace Game.Managers
{
    public class ConstructionManager : BaseManager
    {
        #region Editor Variables
        [Header("Orders")]
        [SerializeField] private Transform orderListContainer;
        [SerializeField] private GameObject orderListPrefab;
        #endregion

        #region Public Members
        public static ConstructionManager Instance { get; private set; }
        #endregion

        #region Private Members
        private Camera camera;

        /* Reference to device currently being placed */
        private GameObject deviceInstance;

        /* Reference to OrderList */
        private GameObject orderListInstance;
        private OrderController orderController;

        /* Layer mask under 8th index */
        private const int floorLayerMask = 1 << 8;
        #endregion

        #region Public Methods
        public void StartDeviceConstruction(GameObject device)
        {
            deviceInstance = Instantiate(device);
            deviceInstance.name = "DeviceBlueprint";
        }
        #endregion

        protected override void Awake()
        {
            base.Awake();

            camera = Camera.main;
        }

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

        private void Update()
        {
            /* Handle device placement.
             * TODO: consider replacing with switch on STATE for readability.
             */
            if (isInPlanningMode()) {
                if (Input.GetMouseButtonDown(Utils.KEY_LMB)) { addDeviceToOrderList(); }
                else if (Input.GetMouseButtonDown(Utils.KEY_RMB)) { exitPlanningMode(); }
                else { repositionDevice(); }
            }
        }

        private void repositionDevice()
        {
            /* Moves blueprint object to position where mouse cursor is 
             * touching floor surface. 
             */
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, floorLayerMask))
            {
                deviceInstance.transform.position = Utils.SnapToGrid(hit.point + Vector3.up, 1f);
            }
        }

        private void addDeviceToOrderList()
        {
            if (null == orderListInstance)
            {
                orderListInstance = Instantiate(orderListPrefab, orderListContainer);
                orderController = orderListInstance.GetComponent<OrderController>();
            }
            orderController.AddOrderItem();
        }

        private void exitPlanningMode()
        {
            Destroy(deviceInstance);
            deviceInstance = null;
        }

        private bool isInPlanningMode() { return null != deviceInstance; }

    }
}
