FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /Sandwish.Server
COPY bin/Debug/netcoreapp2.2/publish .
ENV ASPNETCORE_URLS http://+:5001
EXPOSE 5001
ENTRYPOINT ["dotnet", "Sandwish.Server.dll"]