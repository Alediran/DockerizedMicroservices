FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/SuppressionGroup
COPY "SuppressionGroup/SuppressionGroup.API.csproj" .

RUN dotnet restore "SuppressionGroup.API.csproj"
COPY ./SuppressionGroup .

WORKDIR /src
#Line below copies the Proto files from the "server" gRPC-based Microservice. For each "server" one must copy the Protos folder using the format below.
COPY ./Communication/Protos/ ./Communication/Protos 

WORKDIR /src/SuppressionGroup
RUN dotnet build "SuppressionGroup.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SuppressionGroup.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SuppressionGroup.API.dll"]
