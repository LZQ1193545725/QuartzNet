using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //调度器
            IScheduler scheduler;
            //调度器工厂
            ISchedulerFactory factory;

            //创建一个调度器
            factory = new StdSchedulerFactory();
            scheduler = factory.GetScheduler();
            scheduler.Start();

            //2、创建一个任务
            IJobDetail job = JobBuilder.Create<JoinDemo>().WithIdentity("job1", "group1").Build();
            IJobDetail job1 = JobBuilder.Create<JoinDemo1>().WithIdentity("job2", "group2").Build();
            //3、创建一个触发器
            //DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
                .Build();                             //.StartAt(runTime)

            ITrigger trigger1 = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group2")
                .WithCronSchedule("0/10 * * * * ?")     //5秒执行一次
                .Build();                             //.StartAt(runTime)

            //4、将任务与触发器添加到调度器中
            scheduler.ScheduleJob(job, trigger);
            scheduler.ScheduleJob(job1, trigger1);
            //5、开始执行
            scheduler.Start();
        }
    }

    public class JoinDemo : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine(DateTime.Now);
        }
    }
    public class JoinDemo1 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("我是帅哥");
        }
    }
}
