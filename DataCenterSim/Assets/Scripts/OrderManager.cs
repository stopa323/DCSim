using Game.Structures;
using UnityEngine;

namespace Game.Managers
{
    public class OrderManager : BaseManager
    {
        #region Editor Variables
        [Header("Orders")]
        [SerializeField] private Transform orderDeliveryContainer;
        [SerializeField] private GameObject orderDeliveryPrefab;
        [SerializeField] private PackageStoreManager store;
        #endregion

        #region Public Members
        public static OrderManager Instance { get; private set; }
        #endregion

        #region Private Members
        private static int nextOrderId = 0;
        #endregion

        #region Public Methods
        public int GetOrderId()
        {
            /** Returns next free order ID */
            return nextOrderId++;
        }

        public void AddOrder(Order order)
        {
            GameObject delivery = Instantiate(orderDeliveryPrefab, orderDeliveryContainer);
            OrderDeliveryProgress progress = delivery.GetComponent<OrderDeliveryProgress>();

            progress.Populate(order);
            progress.StartTimer();
        }

        public void OnOrderDelivered(Order order)
        {
            Debug.Log(string.Format("Sound the alarm! {0} is delivered!", order.Name));
            foreach(Package package in order.Items)
                store.PushPackage(package);
        }
        #endregion

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }
    }
}
