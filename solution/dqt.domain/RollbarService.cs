using Rollbar;
using System;
using System.Collections.Generic;
using System.Text;

namespace dqt.domain
{
    public class RollbarService : IRollbarService
    {
        public RollbarService()
        {
            Rollbar.RollbarLocator.RollbarInstance.Configure(
                new Rollbar.RollbarConfig(Environment.GetEnvironmentVariable("RollbarAccessToken"))
                {
                    Environment = Environment.GetEnvironmentVariable("RollbarEnvironemnt")
                });
        }
        public void Error(Exception exception)
        {
            RollbarLocator.RollbarInstance.AsBlockingLogger(TimeSpan.FromSeconds(1)).Error(exception);
        }

        public void Info(string message)
        {
            RollbarLocator.RollbarInstance.Info(message);
        }

        public void Warning(string message)
        {
            RollbarLocator.RollbarInstance.Info(message);
        }
    }
}
