#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Server/ChatRoom.Server.csproj", "Server/"]
COPY ["Shared/ChatRoom.Shared.csproj", "Shared/"]
COPY ["Client/ChatRoom.Client.csproj", "Client/"]
RUN dotnet restore "Server/ChatRoom.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "ChatRoom.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatRoom.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatRoom.Server.dll"]