using System;
using System.Threading.Tasks;
using Quartz;

namespace OsCoreApplication.Libraries.Quatz
{
    public abstract class BaseSchedule : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            try
            {
                return new Task(() => { Run(context); });
            }
            catch (Exception ex)
            {
                return new Task(() => { });
            }
        }

        public abstract Action Run(IJobExecutionContext context);
    }
}
