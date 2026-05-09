# 1. Use the ASP.NET runtime for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
# REMOVED the space after the = sign
ENV ASPNETCORE_URLS=http://+:8080

# 2. Use the SDK image to build the code
# CHANGED 'base' to 'build' to avoid naming conflict
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BinasLibraryNowAPI/BinasLibraryNowAPI.csproj"
# Build and publish the release
RUN dotnet publish "BinasLibraryNowAPI/BinasLibraryNowAPI.csproj" -c Release -o /app/out

# 3. Final stage: copy the build results to the runtime image
FROM base AS final
WORKDIR /app
# Fixed the --from source to match the 'build' stage above
COPY --from=build /app/out .
# FIXED TYPO: Changed "BinasLibraryNowApi.dlls" to "BinasLibraryNowAPI.dll"
ENTRYPOINT ["dotnet", "BinasLibraryNowAPI.dll"]
