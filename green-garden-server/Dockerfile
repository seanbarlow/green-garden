FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["green-garden-server.csproj", "./"]
RUN dotnet restore "green-garden-server.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "green-garden-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "green-garden-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "green-garden-server.dll"]
