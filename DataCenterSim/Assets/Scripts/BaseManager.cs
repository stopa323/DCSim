using UnityEngine;

namespace Game.Managers
{
    public abstract class BaseManager : MonoBehaviour
    {
        protected abstract void initInstance();

        protected virtual void Awake()
        {
            initInstance();
        }
    }

}
