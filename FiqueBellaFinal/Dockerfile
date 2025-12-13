# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia csproj e restaura dependências
COPY FiqueBellaFinal/FiqueBellaFinal.csproj ./FiqueBellaFinal.csproj
RUN dotnet restore FiqueBellaFinal.csproj

# Copia todo o código e publica
COPY FiqueBellaFinal/. ./FiqueBellaFinal/
WORKDIR /app/FiqueBellaFinal
RUN dotnet publish -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app/out ./

# Porta do Railway
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Inicia aplicação
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
