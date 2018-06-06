FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SecuringApplicationSecrets/SecuringApplicationSecrets.csproj SecuringApplicationSecrets/
RUN dotnet restore SecuringApplicationSecrets/SecuringApplicationSecrets.csproj
COPY . .
WORKDIR /src/SecuringApplicationSecrets
RUN dotnet build SecuringApplicationSecrets.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SecuringApplicationSecrets.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SecuringApplicationSecrets.dll"]
