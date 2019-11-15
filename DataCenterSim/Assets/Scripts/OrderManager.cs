using UnityEngine;

namespace Game.Managers
{
    public class OrderManager : BaseManager
    {
        #region Public Members
        public static OrderManager Instance { get; private set; }
        #endregion

        #region Public Methods
        public void AddOrder()
        {
            Debug.Log("Order Made");
        }
        #endregion

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

    }
}
