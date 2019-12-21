using UnityEngine;

namespace Game.Servants
{
    public class BaseServant_Working : SceneLinkedSMB<BaseServantBehavior>
    {
        public override void OnSLStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateEnter(animator, stateInfo, layerIndex);
            m_MonoBehaviour.ExecuteJob();
        }

        public override void OnSLStateNoTransitionUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

            if (m_MonoBehaviour.IsJobFinished()) { m_MonoBehaviour.BecomeIdle(); }
        }

        public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateExit(animator, stateInfo, layerIndex);

            m_MonoBehaviour.FinishJob();
        }
    }
}
