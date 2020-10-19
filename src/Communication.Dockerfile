FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 443
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY "Communication/Communication.API.csproj" .

RUN dotnet restore "Communication.API.csproj"
COPY ./Communication .
RUN dotnet build "Communication.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Communication.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Communication.API.dll"]
