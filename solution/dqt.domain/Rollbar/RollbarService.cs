﻿using Rollbar;
using System;

namespace dqt.domain.Rollbar
{
    public class RollbarService : IRollbarService
    {
        private readonly IConfigSettings _configSettings;

        public RollbarService(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }
        public void Configure(string environment)
        {
            RollbarLocator.RollbarInstance.Configure(
                new RollbarConfig(_configSettings.DQTRollbarAccessToken)
                {
                    Environment = environment
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