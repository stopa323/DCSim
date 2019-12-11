using Game.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Game.JobSystem
{
    public class JobManager : BaseManager
    {
        private List<Job> jobQueue;

        public static JobManager Instance { get; private set; }

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

        protected override void Awake()
        {
            base.Awake();
            jobQueue = new List<Job>();
        }

        public void ScheduleJob(Job job)
        {
            jobQueue.Add(job);
        }

        public Job GetJob()
        {
            if (jobQueue.Count == 0) { return null; }

            Job job = jobQueue[0];
            jobQueue.RemoveAt(0);
            return job;
        }
    }
}

