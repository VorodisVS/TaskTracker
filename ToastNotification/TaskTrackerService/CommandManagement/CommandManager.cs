using System;
using System.Collections.Generic;
using System.Text;

namespace TaskTrackerService
{
    public interface ICommandManager
    {

    }

    public class CommandManager : ICommandManager
    {
        private readonly ITaskManager _taskManager;

        public CommandManager(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        public Command GetCommand(string[] args)
        {
            if (args == null || args.Length == 0)
                return new EmptyCommand();

            switch (args[0])
            {
                case "report":
                    return new ShowReportCommand(_taskManager, DateTime.Parse(args[1]));
                    break;
                default:
                    return new EmptyCommand();
            }
        }
    }
}
