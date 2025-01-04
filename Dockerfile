FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5002

ENV ASPNETCORE_URLS=http://+:5002

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Activite.Services.User/Activite.Services.User.csproj", "src/Activite.Services.User/"]
RUN dotnet restore "src/Activite.Services.User/Activite.Services.User.csproj"
COPY . .
WORKDIR "/src/src/Activite.Services.User"
RUN dotnet build "Activite.Services.User.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Activite.Services.User.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Activite.Services.User.dll"]
