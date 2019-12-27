using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Structures
{
    public class Order
    {
        public float DeliveryTime;
        public string Name;
        public List<Package> Items;

        public Order(int capacity)
        {
            int id = OrderManager.Instance.GetOrderId();
            Name = string.Format("Order#{0}", id);

            DeliveryTime = 0;
            Items = new List<Package>();
        }

        public void AddItem(GameObject guiElement, GameObject item)
        {
            Items.Add(new Package(guiElement, item));
            DeliveryTime += 1f;
        }
    }

    public class Package
    {
        /* Stores references to all objects involved in end-to-end package 
         * delivery process. */
        public GameObject GUIElement;           // GUI representation of the item
        public GameObject OrderedItem;          // Blueprint of the ordered item
        public PackageStoreManager Store;       // Store Manager assigned when package is delivered

        public Package(GameObject guiElement, GameObject item)
        {
            GUIElement = guiElement;
            OrderedItem = item;
            Store = null;
        }
    }
}
