 
# Use official .NET SDK as the build environment
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy everything and restore dependencies
COPY . ./
RUN dotnet restore

# Build the application
RUN dotnet publish -c Release -o out

# Use a smaller runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy compiled files from build-env
COPY --from=build-env /app/out .

# Expose the application port
EXPOSE 5000
EXPOSE 5001

# Set the entry point
ENTRYPOINT ["dotnet", "damage-assessment-api.dll"]
