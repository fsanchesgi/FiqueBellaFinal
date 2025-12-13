# Etapa de build com SDK 7.0
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY FiqueBellaFinal/FiqueBellaFinal.csproj ./FiqueBellaFinal.csproj
RUN dotnet restore FiqueBellaFinal.csproj

# Copia todo o código
COPY FiqueBellaFinal/. ./FiqueBellaFinal/
WORKDIR /app/FiqueBellaFinal
RUN dotnet publish -c Release -o /app/out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
