using UnityEngine;
using Game.Managers;
using System.Collections.Generic;


namespace Game.JobSystem
{
    public class JobManager : BaseManager
    {
        private Queue<Job> jobQueue;

        public static JobManager Instance { get; private set; }

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

        protected override void Awake()
        {
            base.Awake();
            jobQueue = new Queue<Job>();
        }

        public void ScheduleJob(Job job)
        {
            Debug.Log("Job scheduled");
            jobQueue.Enqueue(job);
        }

        public Job GetJob()
        {
            if (jobQueue.Count == 0) {
                Debug.Log("No jobs in queue");
                return null; }
            Debug.Log("Dequeue");
            return jobQueue.Dequeue();
        }
    }
}

