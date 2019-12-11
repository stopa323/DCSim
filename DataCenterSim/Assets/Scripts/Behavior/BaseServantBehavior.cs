using Game.JobSystem;
using UnityEngine;

public class BaseServantBehavior : MonoBehaviour
{
    /* Public API of actions that every servant is capable of doing */
    #region CONST
    public static readonly int _IdleState = Animator.StringToHash("isIdle");
    public static readonly int _MovingState = Animator.StringToHash("isMoving");
    #endregion

    #region Private attributes
    private Animator animator;
    #endregion

    #region Public API
    public bool FindJob()
    {
        var job = JobManager.Instance.GetJob();
        if (job != null) { return true; }
        else { return false; }
    }

    public void StartMoving()
    {
        animator.SetBool(_IdleState, false);
        animator.SetBool(_MovingState, true);
    }
    #endregion

    #region Private Methods
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        SceneLinkedSMB<BaseServantBehavior>.Initialise(animator, this);
    }
    #endregion
}
