using UnityEngine;
using Game.Managers;
using System.Collections.Generic;


namespace Game.JobSystem
{
    public class JobManager : BaseManager
    {
        private Queue<IJob> jobQueue;

        public static JobManager Instance { get; private set; }

        protected override void initInstance()
        {
            if (null == Instance) { Instance = this; }
            else if (this != Instance) { Destroy(gameObject); }
        }

        protected override void Awake()
        {
            base.Awake();
            jobQueue = new Queue<IJob>();
        }

        public void ScheduleJob(IJob job)
        {
            jobQueue.Enqueue(job);
        }

        public IJob GetJob()
        {
            if (jobQueue.Count == 0) { return null; }
            
            return jobQueue.Dequeue();
        }
    }
}

