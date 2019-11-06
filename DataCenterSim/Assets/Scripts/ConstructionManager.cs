using UnityEngine;

namespace Game.Managers
{
    public class ConstructionManager : BaseManager
    {
        #region Editor Variables
        
        #endregion

        #region Public Members
        public static ConstructionManager Instance { get; private set; }
        #endregion

        #region Private Members
        private Camera camera;

        /* Layer mask under 8th index */
        private const int floorLayerMask = 1 << 8;
        #endregion

        #region Public Methods
        public void Test()
        {
            Debug.Log("Event called");
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
            
        }

        private void repositionBluerprintObject()
        {
            /* Moves blueprint object to position where mouse cursor is 
             * touching floor surface. 
             */
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, floorLayerMask))
            {
                //obj.transform.position = Utils.SnapToGrid(hit.point + Vector3.up, 1f);
            }
        }
    }
}
