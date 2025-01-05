FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["KursProjesi.sln", "./"]
COPY ["API/KursProjesi.csproj", "API/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "API/KursProjesi.csproj"

COPY . .
WORKDIR "/src/API"
RUN dotnet build "KursProjesi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KursProjesi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KursProjesi.dll"]