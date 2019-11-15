using UnityEngine;

namespace Game.Managers
{
    // TODO: Move to separate file containing order-related structures
    public struct Order
    {
        public string Name;

        private float timer;
    }

    public class OrderManager : BaseManager
    {
        #region Editor Variables
        [Header("Orders")]
        [SerializeField] private Transform orderDeliveryContainer;
        [SerializeField] private GameObject orderDeliveryPrefab;
        #endregion

        #region Public Members
        public static OrderManager Instance { get; private set; }
        #endregion

        #region Public Methods
        public void AddOrder(Order order)
        {
            GameObject delivery = Instantiate(orderDeliveryPrefab, orderDeliveryContainer);
            OrderDeliveryProgress progress = delivery.GetComponent<OrderDeliveryProgress>();
            progress.StartTimer(10);
        }
        #endregion

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

    }
}
