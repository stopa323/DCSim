using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Structures
{
    public struct Order
    {
        public float DeliveryTime;
        public string Name;
        public List<OrderSubjectTuple> Items;

        public Order(int capacity)
        {
            int id = OrderManager.Instance.GetOrderId();
            Name = string.Format("Order#{0}", id);

            DeliveryTime = 0;
            Items = new List<OrderSubjectTuple>();
        }

        public void AddItem(GameObject guiElement, GameObject item)
        {
            Items.Add(new OrderSubjectTuple(guiElement, item));
            DeliveryTime += 1f;
        }
    }

    public struct OrderSubjectTuple
    {
        /* Stores references to all objects involved in end-to-end package 
         * delivery process. */
        public GameObject GUIElement;   // GUI representation of the item
        public GameObject OrderedItem;  // Blueprint of the ordered item
        public GameObject Package;      // Package containing ordered item

        public OrderSubjectTuple(GameObject guiElement, GameObject item)
        {
            GUIElement = guiElement;
            OrderedItem = item;
            Package = null;
        }

        public void AssignPackage(GameObject package) { Package = package; }
    }

}
