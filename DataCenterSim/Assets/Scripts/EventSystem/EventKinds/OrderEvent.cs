using Game.Structures;
using UnityEngine;

namespace Game.Events
{
    [CreateAssetMenu(fileName = "New Order Event",
        menuName = "Game Events/Order Event")]
    public class OrderEvent : BaseGameEvent<Order>
    {
        public void Raise() => Raise();
    }
}