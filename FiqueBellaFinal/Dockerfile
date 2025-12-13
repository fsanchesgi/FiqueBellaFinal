# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Diretório de trabalho dentro do container
WORKDIR /app

# Copia os arquivos do projeto para o container
COPY FiqueBellaFinal/*.csproj ./FiqueBellaFinal/
COPY FiqueBellaFinal/ ./FiqueBellaFinal/

# Define o diretório de trabalho para o projeto
WORKDIR /app/FiqueBellaFinal

# Restaura as dependências
RUN dotnet restore

# Publica a aplicação em Release
RUN dotnet publish -c Release -o /app/out

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

WORKDIR /app
COPY --from=build /app/out ./

# Porta que a aplicação irá expor
EXPOSE 5000

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "FiqueBellaFinal.dll"]
