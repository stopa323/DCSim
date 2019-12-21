using UnityEngine;
using UnityEngine.AI;

namespace Game.JobSystem
{
    public enum JobState { Enqueued, InProgress, Done }

    public class Job
    {
        private Vector3 destination;
        private NavMeshAgent agent;

        public JobState state { get; private set; }

        public Job(Vector3 destination)
        {
            this.destination = destination;
            this.state = JobState.Enqueued;
        }

        public void Execute(NavMeshAgent agent)
        {
            this.state = JobState.InProgress;
            this.agent = agent;
            this.agent.SetDestination(destination);
        }

        public bool isFinished()
        {
            if (agent.remainingDistance <= agent.stoppingDistance) {
                state = JobState.Done;
                return true;
            }
            else { return false; }
        }
    }
}