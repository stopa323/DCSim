using UnityEngine;

namespace Game.Events
{
    [CreateAssetMenu(fileName = "New GameObject Event",
        menuName = "Game Events/GameObject Event")]
    public class GameObjectEvent : BaseGameEvent<GameObject>
    {
        public void Raise() => Raise();
    }
}