# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia tudo para dentro do container
COPY . .

# Restaura e publica
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/out .

# Railway usa a variável PORT
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
