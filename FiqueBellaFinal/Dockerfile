# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src

# Copia arquivos de solução e projeto
COPY *.sln ./
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/

# Restaura pacotes
RUN dotnet restore "FiqueBellaFinal/FiqueBellaFinal.csproj"

# Copia todo o código
COPY FiqueBellaFinal/. ./FiqueBellaFinal/

WORKDIR /src/FiqueBellaFinal

# Build e publish
RUN dotnet publish "FiqueBellaFinal.csproj" -c Release -o /app/out /p:UseAppHost=false

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Porta padrão ASP.NET Core
EXPOSE 80
EXPOSE 443

# Executa a aplicação
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
