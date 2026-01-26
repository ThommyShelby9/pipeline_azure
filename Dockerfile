# Frontend build stage
FROM node:22-alpine AS frontend-build
WORKDIR /app/frontend
COPY ["src/Presentation/Web/package.json", "src/Presentation/Web/package-lock.json", "./"]
RUN npm ci
COPY ["src/Presentation/Web/", "./"]
RUN npm run build

# Backend build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["Directory.Packages.props", "./"]
COPY ["src/Domain/ShoppingProject.Domain.csproj", "src/Domain/"]
COPY ["src/Application/ShoppingProject.Application.csproj", "src/Application/"]
COPY ["src/Infrastructure/ShoppingProject.Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Presentation/API/ShoppingProject.WebApi.csproj", "src/Presentation/API/"]

RUN dotnet restore "src/Presentation/API/ShoppingProject.WebApi.csproj"

# Copy everything else and build
COPY . .

# Copy frontend build output to API wwwroot
COPY --from=frontend-build /app/frontend/dist src/Presentation/API/wwwroot

WORKDIR "/src/src/Presentation/API"
RUN dotnet build "ShoppingProject.WebApi.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "ShoppingProject.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Copy application files
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingProject.WebApi.dll"]
