FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Dotnet.Homeworks.MainProject/Dotnet.Homeworks.MainProject.csproj", "Dotnet.Homeworks.MainProject/"]
COPY ["Dotnet.Homeworks.Data/Dotnet.Homeworks.Data.csproj", "Dotnet.Homeworks.Data/"]
COPY ["Dotnet.Homeworks.Domain/Dotnet.Homeworks.Domain.csproj", "Dotnet.Homeworks.Domain/"]
COPY ["Dotnet.Homeworks.Shared/Dotnet.Homeworks.Shared.csproj", "Dotnet.Homeworks.Shared/"]
RUN dotnet restore "Dotnet.Homeworks.MainProject/Dotnet.Homeworks.MainProject.csproj"
COPY . .
WORKDIR "/src/Dotnet.Homeworks.MainProject"
RUN dotnet build "Dotnet.Homeworks.MainProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Dotnet.Homeworks.MainProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet.Homeworks.MainProject.dll"]
