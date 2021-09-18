using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation.Collections;

namespace ToastNotification
{
    public class ToastNotification
    {
        public ToastNotification()
        {
            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                // Obtain any user input (text boxes, menu selections) from the notification
                ValueSet userInput = toastArgs.UserInput;
                var input = userInput.Select(x => x.Key.ToString() + x.Value.ToString());
                foreach(var s in input)
                {
                    Console.WriteLine(toastArgs.Argument + "    " + s);
                }
                Console.WriteLine(toastArgs.Argument + "    " + input);
            };
        }

        public void Show()
        {
            // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("Andrew sent you a picture")
                .AddText("Check this out, The Enchantments in Washington!")
                .AddInputTextBox("tbReply", placeHolderContent: "Type a response")
                .AddButton(new ToastButton()
                    .SetContent("Reply")
                    .AddArgument("action", "reply")
                    .SetBackgroundActivation())
                .AddButton(new ToastButton()
                    .SetContent("Like")
                    .AddArgument("action", "like")
                    .SetBackgroundActivation())
                .Show();
        }
    }
}
