FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY SecuringAppSecrets/SecuringAppSecrets.csproj SecuringAppSecrets/
RUN dotnet restore SecuringAppSecrets/SecuringAppSecrets.csproj
COPY . .
WORKDIR /src/SecuringAppSecrets
RUN dotnet build SecuringAppSecrets.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish SecuringAppSecrets.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SecuringAppSecrets.dll"]
