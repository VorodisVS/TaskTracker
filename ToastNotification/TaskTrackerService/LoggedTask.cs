using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackerService
{
    public class LoggedTask
    {
        private static char delim = ';';
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TaskName { get; set; }

        public static LoggedTask FromString(string taskString)
        {
            var fields = taskString.Split(delim);

            var task = new LoggedTask()
            {
                StartTime = DateTime.Parse(fields[0]),
                EndTime = DateTime.Parse(fields[1]),
                TaskName = fields[2]
            };
            return task;
        }

        public override string ToString()
        {
            return string.Join(delim, StartTime.ToString(), EndTime.ToString(), TaskName);
        }
    }
}
