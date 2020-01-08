FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app
COPY /app /app
CMD ASPNETCORE_URLS=http://*:$PORT dotnet supermarket-api.dll