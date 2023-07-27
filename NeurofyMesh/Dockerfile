# Use the official .NET 6.0 SDK image from the Docker Hub
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Copy everything from the current directory to the 'app' folder in the image
WORKDIR /app
COPY . ./

# Restore the .NET project
RUN dotnet restore

# Build the project in Release mode
RUN dotnet publish -c Release -o out

# Use the official .NET 6.0 runtime image from the Docker Hub
FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
# Copy the output of the build
COPY --from=build /app/out .

# Expose port 80 for the application
EXPOSE 5000

# Define the entrypoint of the application
ENTRYPOINT ["dotnet", "NeurofyMesh.dll"]
