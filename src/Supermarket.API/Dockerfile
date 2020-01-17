#FROM microsoft/dotnet:2.2-aspnetcore-runtime
#WORKDIR /app
#COPY /app /app
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet supermarket-api.dll

# Use microsoft/dotnet:2.1-aspnetcore-runtime-stretch-slim 
# from public Docker registry as base image.   
# Alias the image as base  
FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base  
# Set /app as working directory.  
WORKDIR /app  
# Expose port 80 on Image.  
EXPOSE 80  
# Expose port 443 on Image.  
EXPOSE 443  
# Use microsoft/dotnet:2.1-sdk-stretch from public Docker registry 
# as build image. 
# Application will be built on this image.   
# Alias the image as build.   
FROM microsoft/dotnet:2.1-sdk-stretch AS build  
# Set working directory as /src  
WORKDIR /src  
# Copy from LinuxAppForContainer/LinuxAppForContainer.csproj 
# on local disk where Docker file is present   
# to Image folder LinuxAppForContainer in /src folder  
COPY ["Supermarket.API/Supermarket.API.csproj", "Supermarket.API/"]  
# Restore tools and packages for the solution on the local disk  
RUN dotnet restore "Supermarket.API/Supermarket.API.csproj"  
# Copy all files/folders recursively from current folder where the docker 
# file is present on local directory to /src   
COPY . .  
# et working directory as /src/LinuxAppForContainer  
WORKDIR "/src/Supermarket.API"
# Build solution in Release mode and place it in /app folder  
RUN dotnet build "Supermarket.API.csproj" -c Release -o /app  
# Publish build to /app folder and alias build as publish  
FROM build AS publish  
RUN dotnet publish "Supermarket.API.csproj" -c Release -o /app  
 
# Prepare the final base image. Copy /app from image named as publish to base 
# image /app directory and alias it final  
FROM base AS final  
WORKDIR /app  
COPY --from=publish /app .  
ENTRYPOINT ["dotnet", "Supermarket.API.dll"] 