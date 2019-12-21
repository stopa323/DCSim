using UnityEngine;

namespace Game.Servants
{
    public class BaseServant_Idle : SceneLinkedSMB<BaseServantBehavior>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, 
            AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

            // Switch state if job found
            if (m_MonoBehaviour.FindJob()) { m_MonoBehaviour.StartWorking(); }
        }
    }
}
