using Game.JobSystem;
using UnityEngine;
using UnityEngine.AI;

public class BaseServantBehavior : MonoBehaviour
{
    /* Public API of actions that every servant is capable of doing */
    #region CONST
    public static readonly int _IdleState = Animator.StringToHash("isIdle");
    public static readonly int _WorkingState = Animator.StringToHash("isWorking");
    #endregion

    #region Private attributes
    private Animator animator;

    public NavMeshAgent agent;

    private IJob currentJob;
    #endregion

    #region Public API
    public bool FindJob()
    {
        // Return immediately if job already assigned
        if (currentJob != null) return true;
        
        var job = JobManager.Instance.GetJob();
        if (job != null) {
            Debug.Log(string.Format("Job found: {0}", job));

            currentJob = job;
            currentJob.AssignPuppet(this);
            return true;
        }
        else { return false; }
    }

    public void StartWorking()
    {
        animator.SetBool(_IdleState, false);
        animator.SetBool(_WorkingState, true);
    }

    public void UpdateJobStatus()
    {
        currentJob.UpdateExecution();
    }

    public bool IsJobFinished()
    {
        if (currentJob.IsFinished())
        {
            return true;
        }
        else { return false; }
    }

    public void BecomeIdle()
    {
        animator.SetBool(_IdleState, true);
        animator.SetBool(_WorkingState, false);
    }

    public void FinishJob()
    {
        Debug.Log("Finish Job");
        currentJob = null;
    }
    
    public void PickUpPackage()
    {
        Debug.Log("Package Picked Up!");
    }

    public void PlacePackage()
    {
        Debug.Log("Package Placed!");
    }
    #endregion

    #region Private Methods
    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        SceneLinkedSMB<BaseServantBehavior>.Initialise(animator, this);

        agent = GetComponent<NavMeshAgent>();
    }
    #endregion
}
