using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField] private RectTransform orderContainer;
    [SerializeField] private GameObject orderItemPrefab;
    [SerializeField] private float orderItemHeight = 20f;

    private const int MAX_ORDERS = 10;

    private struct OrderTuple
    {
        public GameObject gui_ref; // Reference to order list GUI element
        public GameObject obj_ref; // Reference to 3d object rendered on the scene

        public OrderTuple(GameObject guiElement, GameObject sceneObject)
        {
            gui_ref = guiElement;
            obj_ref = sceneObject;
        }
    }

    private List<OrderTuple> orders;

    private GameObject markerCanvas;

    #region Public Methods
    public bool hasFreeSlot()
    {
        /* Returns true if there is space for at least 1 new item. */
        return orders.Count < MAX_ORDERS;
    }

    public void AddOrderItem(GameObject deviceObject)
    {
        GameObject gui_element = Instantiate(orderItemPrefab, orderContainer);
        OrderTuple order = new OrderTuple(gui_element, deviceObject);
        orders.Add(order);

        /* Reposition newly added order item */
        float y_offset = -(orders.Count - 1) * orderItemHeight;
        gui_element.transform.localPosition = new Vector3(0, y_offset, 0);

        /* Adding new item to the list requires order container resize */
        orderContainer.sizeDelta = new Vector2(orderContainer.sizeDelta.x,
            orderItemHeight * orders.Count);
    }
    
    public void MakeOrder()
    {
        foreach (OrderTuple o in orders)
        {
            BaseDevice device = o.obj_ref.GetComponent<BaseDevice>();
            if (!device) { throw new MissingComponentException("BaseDevice"); }
            device.OnSpawn(markerCanvas);
        }

        Order order = new Order();
        OrderManager.Instance.AddOrder(order);

        Destroy(gameObject);
    }

    public void DiscardOrder()
    {
        foreach (OrderTuple order in orders)
        {
            Destroy(order.obj_ref);
        }
        Destroy(gameObject);
    }
    #endregion

    private void Awake()
    {
        orderContainer.sizeDelta = new Vector2(
            orderContainer.sizeDelta.x, orderItemHeight);
        orders = new List<OrderTuple>(MAX_ORDERS);

        // TODO: Move such read-only references to some kind of singleton
        markerCanvas = GameObject.FindWithTag("MarkerCanvas");
    }

}
