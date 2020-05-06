FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /src

# Copiar csproj e restaurar dependencias
COPY src/Password.Application.sln ./
COPY src/Password.Application/Password.Application.csproj Password.Application/
COPY src/Password.Infra/Password.Infra.csproj Password.Infra/
COPY src/Password.Domain/Password.Domain.csproj Password.Domain/
COPY tests/Password.Tests/Password.Tests.csproj Password.Tests/

RUN dotnet restore ./Password.Application.sln

COPY src/Password.Application/. Password.Application/
COPY src/Password.Infra/. Password.Infra/
COPY src/Password.Domain/. Password.Domain/
COPY tests/Password.Tests/. Password.Tests/

RUN dotnet test ./Password.Tests/Password.Tests.csproj

WORKDIR /src/Password.Application
RUN dotnet build -c Release -o /app

# Build da aplicacao
RUN dotnet publish -c Release -o /app

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app .

ENV ASPNETCORE_URLS=http://+:5001

ENTRYPOINT ["dotnet", "Password.Application.dll"]