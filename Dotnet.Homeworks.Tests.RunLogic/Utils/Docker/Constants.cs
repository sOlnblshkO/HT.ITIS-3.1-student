namespace Dotnet.Homeworks.Tests.RunLogic.Utils.Docker;

public static class Constants
{
    public const string PostgresService = "dotnet_postgres";
    public const string RabbitMqService = "dotnet_rabbitmq";
    public const string MainService = "dotnet_main";
    public const string MailingService = "dotnet_mailing";

    public const string PostgresUserEnvVar = "POSTGRES_USER";
    public const string PostgresPasswordEnvVar = "POSTGRES_PASSWORD";
    public const string PostgresDbEnvVar = "POSTGRES_DB";

    public const string RabbitmqDefaultUserEnvVar = "RABBITMQ_DEFAULT_USER";
    public const string RabbitmqDefaultPassEnvVar = "RABBITMQ_DEFAULT_PASS";

    public const string MainDefaultConnectionStringEnvVar = "ConnectionStrings__Default";

    public static class MailingEmailConfig
    {
        public const string Email = "Email";
        public const string Host = "Host";
        public const string Port = "Port";
        public const string Password = "Password";
    }
}