using UnityEngine;
using UnityEngine.AI;

namespace Game.JobSystem
{
    public interface IJob
    {
        void AssignPuppet(BaseServantBehavior puppet);
        void UpdateExecution();
        bool IsFinished();
    }

    public class DeliverPartsJob : IJob
    {
        private GameObject package;
        private GameObject device;
        private BaseServantBehavior puppet;

        private enum State { Init, ApproachingPackage, ApproachingDevice, Done };
        private State state;

        public DeliverPartsJob(GameObject package, GameObject device)
        {
            this.package = package;
            this.device = device;
            this.puppet = null;
            this.state = State.Init;
        }

        public void AssignPuppet(BaseServantBehavior puppet)
        {
            this.puppet = puppet;
        }

        public void UpdateExecution()
        {
            switch (state)
            {
                case State.Init:
                    start();
                    break;
                case State.ApproachingPackage:
                    if (hasReachedDestination())
                    {
                        puppet.PickUpPackage();
                        puppet.agent.SetDestination(device.transform.position);
                        state = State.ApproachingDevice;
                    }
                    break;
                case State.ApproachingDevice:
                    if (hasReachedDestination())
                    {
                        puppet.PlacePackage();
                        state = State.Done;
                    }
                    break;
                default:
                    break;
            }
        }

        public bool IsFinished() { return State.Done == state; }

        private void start()
        {
            puppet.agent.SetDestination(package.transform.position);
            state = State.ApproachingPackage;
        }

        private bool hasReachedDestination()
        {
            return puppet.agent.remainingDistance <= puppet.agent.stoppingDistance;
        }
    }
}