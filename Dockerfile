FROM mcr.microsoft.com/dotnet/sdk:6.0 as BUILD
WORKDIR /app
COPY . .
RUN dotnet publish Mottu.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as RUN
WORKDIR /app
COPY --from=BUILD /app/out .
ENTRYPOINT [ "dotnet","Mottu.dll" ]