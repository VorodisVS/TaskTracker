using System;
using Xunit;
using ToastNotification;

namespace ToastNotification.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            ToastNotification t = new ToastNotification();
            t.Show();
        }
    }
}
