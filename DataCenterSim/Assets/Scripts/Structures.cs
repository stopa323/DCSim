
namespace Game.Structures
{
    public struct Order
    {
        public int Id;
        public float DeliveryTime;
        public string Name;

        public Order(int id, float deliveryTime)
        {
            Id = id;
            Name = string.Format("Order#{0}", id);
            DeliveryTime = deliveryTime;
        }
    }
}
