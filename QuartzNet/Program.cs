using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuartzNet
{
    class Program
    {
        static void Main(string[] args)
        {

            StartWork();
            Console.ReadKey();
        }

        public static async Task StartWork()
        {
            ISchedulerFactory factory = new StdSchedulerFactory();
            IScheduler sched = await factory.GetScheduler();

            //开启调度器
            await sched.Start();
            IJobDetail job = JobBuilder.Create<JoinDemo>().WithIdentity("job1", "group1").Build();
            ITrigger trigger = TriggerBuilder.Create()
               .WithIdentity("trigger1", "group1")
               .WithCronSchedule("0/5 * * * * ?")     //5秒执行一次
               .Build();                              //.StartAt(runTime)

            await sched.ScheduleJob(job, trigger);
        }
    }

    public class JoinDemo : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(()=> {
                Console.Out.WriteLineAsync("时间："+DateTime.Now+",我是帅哥");
            });
            
            
        }
    }
}
