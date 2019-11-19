using Game.Structures;

namespace Game.Events
{
    public class OrderEventListener : BaseGameEventListener<
        Order, OrderEvent, UnityOrderEvent>
    { }
}
