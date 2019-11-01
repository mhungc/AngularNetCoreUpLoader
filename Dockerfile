FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["RestUploadFile.csproj", ""]
RUN dotnet restore "./RestUploadFile.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RestUploadFile.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestUploadFile.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestUploadFile.dll"]