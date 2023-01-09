﻿using Org.Notification.Service.Interface;

namespace Org.Notification.Service
{
    public class SmsService : ISmsService
    {
        public Task SendSmsAsync(string? mobile, string? body, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
