using UnityEngine;

namespace Game.Managers
{
    public class GameStateManager : BaseManager
    {
        #region Public Members
        public static GameStateManager Instance { get; private set; }

        public Camera MainCamera { get; private set; }
        public GameObject MarkerCanvas { get; private set; }
        #endregion

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

        private void Awake()
        {
            base.Awake();

            MainCamera = Camera.main;
            MarkerCanvas = GameObject.FindWithTag("MarkerCanvas");
        }
    }
}
