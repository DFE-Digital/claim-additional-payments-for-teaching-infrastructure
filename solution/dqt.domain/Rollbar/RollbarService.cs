using Rollbar;
using System;

namespace dqt.domain.Rollbar
{
    public class RollbarService : IRollbarService
    {
        public RollbarService(IConfigSettings configSettings)
        {
            RollbarLocator.RollbarInstance.Configure(
                new RollbarConfig(configSettings.DQTRollbarAccessToken)
                {
                    Environment = configSettings.DQTRollbarEnvironment
                });
        }
        public void Configure(string environment)
        {
            
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
