using Quartz;

namespace OsCoreApplication.Libraries.Quatz.Interface
{
    public interface IQuatzExcution
    {
        void Run(IJobExecutionContext context);
        ITrigger GetTriggers();
        IJobDetail GetDetails();
    }
}
