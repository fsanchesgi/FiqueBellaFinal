# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

# Copia csproj e restaura pacotes
COPY *.sln .
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/
RUN dotnet restore

# Copia todo o código
COPY FiqueBellaFinal/. ./FiqueBellaFinal/

WORKDIR /app/FiqueBellaFinal

# Build e publish
RUN dotnet publish -c Release -o /app/out /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Porta padrão ASP.NET Core
EXPOSE 80

# Executa a aplicação
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
