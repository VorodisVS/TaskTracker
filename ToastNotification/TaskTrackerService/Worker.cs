using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation.Collections;

namespace TaskTrackerService
{
    public class Worker : BackgroundService
    {

        private const string REPLY_ID = "replyId";
        private readonly ILogger<Worker> _logger;
        private readonly ITaskWriter _taskWriter;


        public Worker(ILogger<Worker> logger, ITaskWriter taskWriter)
        {
            _logger = logger;
            _taskWriter = taskWriter;

            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
                ValueSet userInput = toastArgs.UserInput;
                if (userInput.TryGetValue(REPLY_ID, out var taskName)){

                    _taskWriter.MakeTaskRecord(taskName.ToString(), 15);
                }
            };
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                ShowToastNotification();
                await Task.Delay(15000, stoppingToken);
            }
        }

        private void ShowToastNotification()
        {
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Put the task that you do")
                .AddInputTextBox(REPLY_ID, placeHolderContent: "Type a response")
                .AddButton(new ToastButton()
                    .SetContent("Reply")
                    .AddArgument("action", "reply")
                    .SetBackgroundActivation())
                .Show();
        }
    }
}
