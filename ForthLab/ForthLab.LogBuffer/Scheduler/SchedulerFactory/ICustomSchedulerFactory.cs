using Quartz;

namespace ForthLab.LogBuffer.Scheduler.SchedulerFactory;

public interface ICustomSchedulerFactory
{
    public Task<IScheduler> GetScheduler();
}