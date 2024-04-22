using Quartz;
using Quartz.Impl;

namespace ForthLab.LogBuffer.Scheduler.SchedulerFactory;

public class CustomSchedulerFactory : ICustomSchedulerFactory
{
    private readonly StdSchedulerFactory _stdSchedulerFactory; 
    
    public CustomSchedulerFactory()
    {
        _stdSchedulerFactory = new StdSchedulerFactory();
    }
    
    public async Task<IScheduler> GetScheduler()
    {
        return await _stdSchedulerFactory.GetScheduler();
    }
}