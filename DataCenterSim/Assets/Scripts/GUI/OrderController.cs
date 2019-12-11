using Game.Managers;
using Game.Structures;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField] private RectTransform orderContainer;
    [SerializeField] private GameObject orderItemPrefab;
    [SerializeField] private float orderItemHeight = 20f;

    private const int MAX_ORDERS = 10;

    private Order order;

    private GameObject markerCanvas;

    #region Public Methods
    public bool hasFreeSlot()
    {
        /* Returns true if there is space for at least 1 new item. */
        return order.Items.Count < MAX_ORDERS;
    }

    public void AddOrderItem(GameObject deviceObject)
    {
        GameObject gui_element = Instantiate(orderItemPrefab, orderContainer);
        order.AddItem(gui_element, deviceObject);

        /* Reposition newly added order item */
        float y_offset = -(order.Items.Count - 1) * orderItemHeight;
        gui_element.transform.localPosition = new Vector3(0, y_offset, 0);

        /* Adding new item to the list requires order container resize */
        orderContainer.sizeDelta = new Vector2(orderContainer.sizeDelta.x,
            orderItemHeight * order.Items.Count);
    }
    
    public void MakeOrder()
    {
        foreach (Package tup in order.Items)
        {
            BaseDevice device = tup.OrderedItem.GetComponent<BaseDevice>();
            if (!device) { throw new MissingComponentException("BaseDevice"); }
            device.OnSpawn(markerCanvas);
        }

        OrderManager.Instance.AddOrder(order);

        Destroy(gameObject);
    }

    public void DiscardOrder()
    {
        foreach (Package tup in order.Items) { Destroy(tup.OrderedItem); }
        Destroy(gameObject);
    }
    #endregion

    private void Awake()
    {
        orderContainer.sizeDelta = new Vector2(
            orderContainer.sizeDelta.x, orderItemHeight);
        order = new Order(MAX_ORDERS);

        // TODO: Move such read-only references to some kind of singleton
        markerCanvas = GameObject.FindWithTag("MarkerCanvas");
    }

}
