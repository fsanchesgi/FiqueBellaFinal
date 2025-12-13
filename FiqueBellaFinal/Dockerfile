# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

# Copia csproj e restaura dependências
COPY FiqueBellaFinal/FiqueBellaFinal.csproj ./
RUN dotnet restore FiqueBellaFinal.csproj

# Copia todo o código
COPY FiqueBellaFinal/. ./FiqueBellaFinal
WORKDIR /src/FiqueBellaFinal

# Publica em modo Release
RUN dotnet publish -c Release -o /app/out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expõe porta padrão
EXPOSE 80

# Comando de inicialização
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
