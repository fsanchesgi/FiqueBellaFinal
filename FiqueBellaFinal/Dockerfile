# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/
WORKDIR /app/FiqueBellaFinal
RUN dotnet restore

# Copiar todo o código (incluindo Areas/Services)
COPY FiqueBellaFinal/. ./FiqueBellaFinal

# Build e publish
RUN dotnet publish -c Release -o /app/out /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
