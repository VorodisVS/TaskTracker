using System;

namespace TaskTrackerService
{
    public class ShowReportCommand : Command
    {
        private ITaskManager _taskManager;
        private DateTime _reportTime;
        public ShowReportCommand(ITaskManager taskManager, DateTime time)
        {
            _taskManager = taskManager;
            _reportTime = time;
        }

        public override void Execute()
        {
            var reportStrings = _taskManager.MakeReport(_reportTime);
            Console.WriteLine(string.Join(Environment.NewLine, reportStrings));
        }

        public override void ParseArgs(string[] args)
        {
            base.ParseArgs(args);
        }
    }
}
