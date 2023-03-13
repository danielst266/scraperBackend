FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set the working directory to /app
WORKDIR /app

# Copy the current directory contents into the container at /app
COPY . /app

#COPY appsettings.json /app
#ENV ASPNETCORE_URLS=http://+:5000
# Expose port 80 for the container to listen on
EXPOSE 5000

# Set the entry point to run the dotnet command on the assembly with the WebAPI application
ENTRYPOINT ["dotnet", "bin/Debug/net6.0/webapi.dll"]