using Game.Events;
using Game.Structures;
using UnityEngine;
using UnityEngine.UI;

public class OrderDeliveryProgress : MonoBehaviour
{
    public Image progressImage;
    public Text timeText;
    public Text nameText;

    public OrderEvent OnOrderDelivered;

    private bool started = false;
    private float remainingTime = 0;

    private Order order;

    public void Populate(Order order)
    {
        this.order = order;
        remainingTime = order.DeliveryTime;
        nameText.text = order.Name;
    }

    public void StartTimer() { started = true; }

    private void LateUpdate()
    {
        if (!started) return;

        remainingTime = Mathf.Clamp(remainingTime - Time.deltaTime, 0, order.DeliveryTime);
        timeText.text = string.Format("{0}s", remainingTime);

        progressImage.fillAmount = (order.DeliveryTime - remainingTime) / order.DeliveryTime;

        if (0 == remainingTime)
        {
            OnOrderDelivered.Raise(order);
            Destroy(gameObject);
        }
    }
}
