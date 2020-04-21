#FROM microsoft/dotnet:2.2-aspnetcore-runtime
#WORKDIR /app
#COPY /app /app
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet supermarket-api.dll

# STAGE01 - Build application and its dependencies
FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app
COPY *.csproj ./
COPY . ./
RUN dotnet restore 

# STAGE02 - Publish the application
FROM build-env AS publish
RUN dotnet publish -c Release -o /app

# STAGE03 - Create the final image
FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
LABEL Author="Ricardo Costa"
LABEL Maintainer="testingAPI"
COPY --from=publish /app .
EXPOSE 80/tcp

#ENTRYPOINT ["dotnet", "supermarketapi.dll", "--server.urls", "https://*:80"]
ENTRYPOINT ["dotnet", "supermarketapi.dll"]