FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

ENV ConnectionStrings__Default "host=127.0.0.1;Port=5432;Database=postgres;username=postgres;Password=root;"

WORKDIR /app

COPY ./*.csproj ./

RUN dotnet restore

COPY . .

RUN dotnet build -c Release --no-restore

RUN dotnet ef migrations add InitialMigration --context UrlContext --output-dir Migrations

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final

ENV ConnectionStrings__Default "host=127.0.0.1;Port=5432;Database=postgres;username=postgres;Password=root;"

RUN apt-get update \
    && apt-get install -y postgresql-client

WORKDIR /app

COPY --from=build /app/bin/Release/net6.0/publish .

ENTRYPOINT ["dotnet", "Mottu.dll"]
