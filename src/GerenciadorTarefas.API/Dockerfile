FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GerenciadorTarefas.API/GerenciadorTarefas.API.csproj", "GerenciadorTarefas.API/"]
COPY ["GerenciadorTarefas.Aplicacao/GerenciadorTarefas.Aplicacao.csproj", "GerenciadorTarefas.Aplicacao/"]
COPY ["GerenciadorTarefas.Core/GerenciadorTarefas.Core.csproj", "GerenciadorTarefas.Core/"]
COPY ["GerenciadorTarefas.Infraestrutura/GerenciadorTarefas.Infraestrutura.csproj", "GerenciadorTarefas.Infraestrutura/"]
RUN dotnet restore "GerenciadorTarefas.API/GerenciadorTarefas.API.csproj"
COPY . .
WORKDIR "/src/GerenciadorTarefas.API"
RUN dotnet build "GerenciadorTarefas.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GerenciadorTarefas.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GerenciadorTarefas.API.dll"]