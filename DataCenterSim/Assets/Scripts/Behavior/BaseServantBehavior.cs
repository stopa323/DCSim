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

    private NavMeshAgent agent;

    private Job currentJob;
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
            return true;
        }
        else { return false; }
    }

    public void StartWorking()
    {
        animator.SetBool(_IdleState, false);
        animator.SetBool(_WorkingState, true);
    }

    public void ExecuteJob()
    {
        Debug.Log(string.Format("Executing {0}", currentJob));
        currentJob.Execute(agent);
    }

    public bool IsJobFinished()
    {
        Debug.Log("Check JOB");
        if (currentJob.isFinished())
        {
            Debug.Log("JobFinished");
            //currentJob = null;
            return true;
        }
        else { Debug.Log("JobInProgress"); return false; }
    }

    public void BecomeIdle()
    {
        Debug.Log("BecomeIdle");
        animator.SetBool(_IdleState, true);
        animator.SetBool(_WorkingState, false);
    }

    public void FinishJob()
    {
        Debug.Log("Finish Job");
        currentJob = null;
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
