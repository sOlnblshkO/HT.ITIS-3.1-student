using Dotnet.Homeworks.Patterns.CommandPattern;
using Dotnet.Homeworks.Patterns.CommandPattern.Commands;
using Dotnet.Homeworks.Patterns.DecoratorPattern;
using Dotnet.Homeworks.Patterns.FactoryPattern;
using Dotnet.Homeworks.Patterns.FactoryPattern.ConcreteCreators;
using Dotnet.Homeworks.Patterns.MediatorPattern;

#region Command Pattern

var createUser = new CreateUserCommand("123");
var deleteUser = new DeleteUserCommand("123");

var taskManager = new TaskManager();
taskManager.ExecuteCommand(createUser); // Created
taskManager.ExecuteCommand(deleteUser); // Deleted
taskManager.UndoLastCommand(); // Восстановлен

#endregion

#region Factory Pattern

LoggerCreator consoleLogger = new ConsoleLogCreator();
consoleLogger.LogMessage("Hello world");

LoggerCreator fileLogger = new FileLogCreator("path");
fileLogger.LogMessage("Hello world");

#endregion

#region MediatorPattern

IChatRoomMediator chatRoom = new ChatRoom();

var user1 = new User("123", chatRoom);
var user2 = new User("1234", chatRoom);

user1.SendMessage("123");
user2.SendMessage("1234");

#endregion

#region DecoratorPattern

INotification notification = new EmailNotification();
notification.Send("Hello world");

notification = new SMSNotificationDecorator(notification);
notification.Send("123");

notification = new PushNotificationDecorator(notification);
notification.Send("1234");

#endregion