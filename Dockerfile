# STAGE01 - Build application and its dependencies
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY *.csproj ./
COPY . ./
RUN dotnet restore 

# STAGE02 - Publish the application
FROM build-env AS publish
RUN dotnet publish -c Release -o /app

# STAGE03 - Create the final image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
LABEL Author="Ricardo Costa"
LABEL Maintainer="testingAPI"
COPY --from=publish /app .
EXPOSE 80/tcp

#ENTRYPOINT ["dotnet", "supermarketapi.dll", "--server.urls", "https://*:80"]
ENTRYPOINT ["dotnet", "supermarketapi.dll"]