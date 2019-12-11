namespace Game.JobSystem
{
    public interface IJob
    {
        void Execute();
    }

    public class Job : IJob
    {
        public Job()
        {

        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}