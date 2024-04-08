FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Tasks.Parallel/Tasks.Parallel/Tasks.Parallel.csproj", "Tasks.Parallel/Tasks.Parallel/"]
RUN dotnet restore "Tasks.Parallel/Tasks.Parallel/Tasks.Parallel.csproj"
COPY . .
WORKDIR "/src/Tasks.Parallel/Tasks.Parallel"
RUN dotnet build "Tasks.Parallel.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Tasks.Parallel.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tasks.Parallel.dll"]
