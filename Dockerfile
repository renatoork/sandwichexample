FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY public .
ENTRYPOINT ["rateLibraries.exe", "-repository"]
