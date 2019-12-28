using Game.Structures;
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

    public class DeliverPackageJob : IJob
    {
        private Package package;
        private BaseServantBehavior puppet;

        private enum State { Init, ApproachingPackage, ApproachingDevice, Done };
        private State state;

        public DeliverPackageJob(Package package)
        {
            this.package = package;
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
                        package.Store.PopPackage(package);

                        puppet.PickUpPackage();
                        puppet.agent.SetDestination(package.OrderedItem.transform.position);
                        state = State.ApproachingDevice;
                    }
                    break;
                case State.ApproachingDevice:
                    if (hasReachedDestination())
                    {
                        puppet.PlacePackage();

                        var device = package.OrderedItem.GetComponent<BaseDevice>();
                        device.OnPartsDelivered();

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
            puppet.agent.SetDestination(package.Store.transform.position);
            state = State.ApproachingPackage;
        }

        private bool hasReachedDestination()
        {
            return puppet.agent.remainingDistance <= puppet.agent.stoppingDistance;
        }
    }

    public class AssembleDeviceJob : IJob
    {
        private BaseServantBehavior puppet;
        private BaseDevice device;
        private Vector3 devicePosition;

        private enum State { Init, ApproachingDevice, Constructing, Done};
        private State state;

        public AssembleDeviceJob(GameObject device)
        {
            this.devicePosition = device.transform.position;
            this.device = device.GetComponent<BaseDevice>();
            this.state = State.Init;
        }

        public void AssignPuppet(BaseServantBehavior puppet)
        {
            this.puppet = puppet;
        }

        public bool IsFinished()
        {
            return device.IsFinished();
        }

        public void UpdateExecution()
        {
            switch (state)
            {
                case State.Init:
                    puppet.agent.SetDestination(devicePosition);
                    state = State.ApproachingDevice;
                    break;
                case State.ApproachingDevice:
                    if (puppet.agent.remainingDistance <= puppet.agent.stoppingDistance)
                    {
                        device.StartConstruction();
                        state = State.Constructing;
                    }
                    break;
                case State.Constructing:
                    if (device.IsFinished()) { state = State.Done; }
                    break;
                default:
                    break;
            }
        }
    }
}