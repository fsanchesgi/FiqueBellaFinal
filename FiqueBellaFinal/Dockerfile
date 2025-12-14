# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

# Copia arquivos de solução e projeto
COPY *.sln ./
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/

# Restaura pacotes NuGet
RUN dotnet restore "FiqueBellaFinal/FiqueBellaFinal.csproj"

# Copia todo o código
COPY FiqueBellaFinal/. ./FiqueBellaFinal/

WORKDIR /src/FiqueBellaFinal

# Build e publish
RUN dotnet publish "FiqueBellaFinal.csproj" -c Release -o /app/out /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app

# Copia saída do build
COPY --from=build /app/out ./

# Porta padrão ASP.NET Core
EXPOSE 80
EXPOSE 443

# Variáveis de ambiente para PostgreSQL (opcional, sobrescreve se estiver no Railway)
ENV DATABASE_URL=postgresql://postgres:FiqueBella@2025@postgres.railway.internal:5432/railway
ENV PGUSER=postgres
ENV PGPASSWORD=FiqueBella@2025
ENV PGHOST=postgres.railway.internal
ENV PGPORT=5432
ENV PGDATABASE=railway

# Executa a aplicação
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
