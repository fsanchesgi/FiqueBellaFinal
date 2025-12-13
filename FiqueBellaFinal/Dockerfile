# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia o projeto e restaura pacotes NuGet
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/
RUN dotnet restore "FiqueBellaFinal/FiqueBellaFinal.csproj"

# Copia todo o restante do código
COPY FiqueBellaFinal/. ./FiqueBellaFinal/
WORKDIR /src/FiqueBellaFinal

# Publica em Release
RUN dotnet publish "FiqueBellaFinal.csproj" -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
