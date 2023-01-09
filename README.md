# Notification Library

## Diagram
This Diagram explain the architecture that this library follow.

* Publisher and subscriber model
![notification library](https://raw.githubusercontent.com/M-Alzanati/NotificationLibrary/main/digram.png)

* Cache the message if it was sent before, same message can be added to many publisher but one worker allowed per message type
![notification library](https://raw.githubusercontent.com/M-Alzanati/NotificationLibrary/main/caching.png)

### Basic Usage
```csharp
// Register service with dependent services
builder.Services
    .AddNotificationSettings()
    .AddRabbitMqProducer(builder.Configuration)
    .AddRedisCache(builder.Configuration)
    .AddSmsSubscription("sms")
    .AddEmailSubscription("email")
    .WithWorkers();
```

```csharp
// Inject it some where
Something(INotificationsPublisher notificationPublisher)
```

```csharp
// Regsiter all required publishers
notificationPublisher
    .Subscribe<SmsPublisher>()
    .Subscribe<EmailPublisher>();
```

```csharp
// Notify all registered publishers
var message = new NotificationMessage
{
    DetailMessage =
    {
        new ImessageDto(),
        new ImessageDto(),
        ...
    }
};

await _notificationPublisher.NotifyAsync(message);
```

### Adding custom publisher and worker

```csharp
// 1- Add message
class newMessageDto : IMessageDto {...}

// 2- Add new publisher by inherit from AbstractPublisher and override methods
class newPublisher : AbstractPublisher<newMessageDto> {...}

// 3- Add new worker by inherit from AbstractCommand
class newWorker : AbstractCommand<newMessageDto> {...}

// 4- Register new subscription
serviceCollection.AddCustomSubscription<newPublisher, newWorker>()
```
