# Use the official .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy solution file and project files
COPY GeminiCatalog.sln ./
COPY src/GeminiCatalog.Api/GeminiCatalog.Api.csproj ./src/GeminiCatalog.Api/
COPY src/GeminiCatalog.Application/GeminiCatalog.Application.csproj ./src/GeminiCatalog.Application/
COPY src/GeminiCatalog.Contracts/GeminiCatalog.Contracts.csproj ./src/GeminiCatalog.Contracts/
COPY src/GeminiCatalog.Domain/GeminiCatalog.Domain.csproj ./src/GeminiCatalog.Domain/
COPY src/GeminiCatalog.Infrastructure/GeminiCatalog.Infrastructure.csproj ./src/GeminiCatalog.Infrastructure/

# Restore NuGet packages
RUN dotnet restore

# Copy the entire source code
COPY . ./

# Build and publish the API project
RUN dotnet publish src/GeminiCatalog.Api/GeminiCatalog.Api.csproj -c Release -o out --no-restore

# Use the official .NET 9.0 ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build-env /app/out .

# Install essential debugging tools as root
RUN apt-get update && \
    apt-get install -y \
    curl \
    iputils-ping \
    telnet \
    dnsutils \
    net-tools \
    wget \
    && rm -rf /var/lib/apt/lists/* \
    && apt-get clean

# Create a non-root user for security
RUN adduser --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser


# Set the entry point for the container
ENTRYPOINT ["dotnet", "GeminiCatalog.Api.dll"]