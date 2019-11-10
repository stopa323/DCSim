using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    [SerializeField] private RectTransform orderContainer;
    [SerializeField] private GameObject orderItemPrefab;
    [SerializeField] private float orderItemHeight = 20f;

    private const int MAX_ORDERS = 10;

    private List<GameObject> orders;

    private void Awake()
    {
        orderContainer.sizeDelta = new Vector2(
            orderContainer.sizeDelta.x, orderItemHeight);
        orders = new List<GameObject>(MAX_ORDERS);
    }

    public void AddOrderItem()
    {
        if (orders.Count >= MAX_ORDERS) {
            Debug.LogWarning("Maximum items per order limit exceeded");
            return;
        }

        GameObject new_order = Instantiate(orderItemPrefab, orderContainer);
        orders.Add(new_order);

        /* Reposition newly added order item */
        float y_offset = -(orders.Count - 1) * orderItemHeight;
        new_order.transform.localPosition = new Vector3(0, y_offset, 0);

        /* Adding new item to the list requires order container resize */
        orderContainer.sizeDelta = new Vector2(orderContainer.sizeDelta.x, 
            orderItemHeight * orders.Count);
    }
}
