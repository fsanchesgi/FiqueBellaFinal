# Imagem base .NET 7 SDK
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia tudo e publica
COPY . ./
RUN dotnet publish -c Release -o /app/out

# Imagem runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
