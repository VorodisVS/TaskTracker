using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskTrackerService
{
    public interface ITaskManager
    {
        string[] MakeReport(DateTime time);
    }

    public class TaskManager : ITaskManager
    {
        private readonly ITaskWriter _taskWriter;

        public TaskManager(ITaskWriter taskWriter)
        {
            _taskWriter = taskWriter;
        }

        public string[] MakeReport(DateTime time)
        {
            var allTasks = _taskWriter.GetLoggedTasks(time);
            Dictionary<string, ContTask> tsks = new Dictionary<string, ContTask>();
            foreach(var t in allTasks)
            {
                if (tsks.TryGetValue(t.TaskName, out var existedTask))
                {
                    existedTask.AddDuration(t.EndTime - t.StartTime);
                }
                else
                {
                    tsks.Add(t.TaskName, new ContTask(t.TaskName) { Duration = t.EndTime - t.StartTime });
                }
            }
            return tsks.Select(x => $"{x.Value.TaskName}: {x.Value.Duration}").ToArray();
        }
    }

    public class ContTask
    {
        public string TaskName { get; set; }
        public TimeSpan Duration { get; set; }

        public void AddDuration(TimeSpan timeSpan)
        {
            Duration += timeSpan;
        }

        public ContTask(string taskName)
        {
            TaskName = taskName;
        }

        public override string ToString()
        {
            return $"{TaskName}:{Duration}";
        }
    }
}
