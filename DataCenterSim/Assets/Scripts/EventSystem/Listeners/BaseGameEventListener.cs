using UnityEngine;
using UnityEngine.Events;

namespace Game.Events
{
    public abstract class BaseGameEventListener<T, E, UER> :
        MonoBehaviour, IGameEventListener<T>
        where E : BaseGameEvent<T>
        where UER : UnityEvent<T>
    {

        [SerializeField] private E gameEvent;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse;

        private void OnEnable()
        {
            if (null == gameEvent) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (null == gameEvent) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            if (null != unityEventResponse) { unityEventResponse.Invoke(item); }
        }
    }

}
