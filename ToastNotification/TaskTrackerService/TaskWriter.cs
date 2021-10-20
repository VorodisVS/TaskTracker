using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;

namespace TaskTrackerService
{
    public interface ITaskWriter
    {
        void MakeTaskRecord(string task, int periondSec);
        List<LoggedTask> GetLoggedTasks(DateTime date);

    }

    public class TaskWriter : ITaskWriter
    {
        readonly TaskWriterOptions _options;
        public TaskWriter(IOptions<TaskWriterOptions> options)
        {
            _options = options.Value;
        }

        public void MakeTaskRecord(string task, int periondSec)
        {
            CheckDirectoryExistOrCreate(_options.DirectoryName);
            

            var fileName = Path.Combine(_options.DirectoryName, DateTime.Now.DayOfYear.ToString());

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                var curTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(periondSec));
                var loggedTask = new LoggedTask()
                {
                    StartTime = curTime,
                    EndTime = DateTime.Now,
                    TaskName = task
                };
                sw.WriteLine(loggedTask);
            }
        }

        public List<LoggedTask> GetLoggedTasks(DateTime date)
        {
            var fileName = Path.Combine(_options.DirectoryName, date.DayOfYear.ToString());
            List<LoggedTask> tasks = new List<LoggedTask>();
            using (StreamReader sr = new StreamReader(File.OpenRead(fileName)))
            {
                while (!sr.EndOfStream)
                {
                    var taskString = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(taskString))
                    {
                        continue;
                    }
                    var loggedTask = LoggedTask.FromString(taskString);
                    tasks.Add(loggedTask);
                }
            }

            return tasks;
        }

        private void CheckDirectoryExistOrCreate(string directorName)
        {
            if (Directory.Exists(directorName))
                return;
            Directory.CreateDirectory(directorName);
        }
    }

    public class TaskWriterOptions
    {
        public static string SectionName = "TaskWriter";

        [Required]
        public string DirectoryName { get; set; }

    }
}
