using UnityEngine;

namespace Game.Servants
{
    public class BaseServant_Idle : SceneLinkedSMB<BaseServantBehavior>
    {
        public override void OnSLStateNoTransitionUpdate(Animator animator, 
            AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnSLStateNoTransitionUpdate(animator, stateInfo, layerIndex);

            // Find job to do
            var jobFound = m_MonoBehaviour.FindJob();

            // Switch state if found
            if (jobFound)
            {
                Debug.Log("Job found!");
                m_MonoBehaviour.StartMoving();
            }
        }
    }
}
