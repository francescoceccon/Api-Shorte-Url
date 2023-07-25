FROM mcr.microsoft.com/dotnet/sdk:6.0 as BUILD
WORKDIR /app
COPY ../Domain/*.csproj .
COPY ../Application/*.csproj .
COPY ../Infrastructure/*.csproj .
COPY ../Mottu/*.csproj .
COPY . .
# ARG PORT=http://localhost:8080
# ENV ASPNETCORE_URLS=$PORT
RUN dotnet publish Mottu.sln -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=BUILD /app/out .
# ARG PORT=http://localhost:8080
# ENV ASPNETCORE_URLS=$PORT
EXPOSE 5000
ENTRYPOINT [ "dotnet","Mottu.dll" ]