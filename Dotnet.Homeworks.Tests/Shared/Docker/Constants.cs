using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Storage.API.Configuration;

namespace Dotnet.Homeworks.Tests.Shared.Docker;

public static class Constants
{
    public const string PostgresService = "dotnet_postgres";
    public const string RabbitMqService = "dotnet_rabbitmq";
    public const string MinioService = "dotnet_minio";
    public const string MongodbService = "dotnet_mongodb";
    public const string JaegerService = "dotnet_jaeger";
    public const string MainService = "dotnet_main";
    public const string MailingService = "dotnet_mailing";
    public const string StorageService = "dotnet_storage";

    public const string PostgresUserEnvVar = "POSTGRES_USER";
    public const string PostgresPasswordEnvVar = "POSTGRES_PASSWORD";
    public const string PostgresDbEnvVar = "POSTGRES_DB";

    public const string RabbitmqDefaultUserEnvVar = "RABBITMQ_DEFAULT_USER";
    public const string RabbitmqDefaultPassEnvVar = "RABBITMQ_DEFAULT_PASS";

    public const string MinioRootUserEnvVar = "MINIO_ROOT_USER";
    public const string MinioRootPassEnvVar = "MINIO_ROOT_PASSWORD";

    public const string MongodbRootUsernameEnvVar = "MONGO_INITDB_ROOT_USERNAME";
    public const string MongodbRootPasswordEnvVar = "MONGO_INITDB_ROOT_PASSWORD";

    public const string JaegerOtlpCollectorEnvVar = "COLLECTOR_OTLP_ENABLED";

    public const string MainDefaultConnectionStringEnvVar = "ConnectionStrings__Default";

    public static class MailingEmailConfig
    {
        public const string Email = nameof(EmailConfig.Email);
        public const string Host = nameof(EmailConfig.Host);
        public const string Port = nameof(EmailConfig.Port);
        public const string Password = nameof(EmailConfig.Password);
    }

    public static class StorageMinioConfig
    {
        public const string Username = nameof(MinioConfig.Username);
        public const string Password = nameof(MinioConfig.Password);
        public const string Endpoint = nameof(MinioConfig.Endpoint);
        public const string Port = nameof(MinioConfig.Port);
        public const string WithSsl = nameof(MinioConfig.WithSsl);
    }
}