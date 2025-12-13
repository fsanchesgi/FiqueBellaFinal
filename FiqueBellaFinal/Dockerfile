# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia a solution e o csproj
COPY FiqueBellaFinal.sln .
COPY FiqueBellaFinal.csproj .

# Restaura dependências
RUN dotnet restore

# Copia o restante dos arquivos
COPY . .

# Publica a aplicação
RUN dotnet publish -c Release -o /app/out

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app

COPY --from=build /app/out .

# Railway define a porta na variável PORT
ENV ASPNETCORE_URLS=http://+:$PORT
EXPOSE 8080

ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
