#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/LZMotel.Identidade.API/LZMotel.Identidade.API.csproj", "src/services/LZMotel.Identidade.API/"]
COPY ["src/building blocks/LZMotel.MessageBus/LZMotel.MessageBus.csproj", "src/building blocks/LZMotel.MessageBus/"]
COPY ["src/building blocks/LZMotel.Core/LZMotel.Core.csproj", "src/building blocks/LZMotel.Core/"]
COPY ["src/building blocks/LZMotel.WebAPI.Core/LZMotel.WebAPI.Core.csproj", "src/building blocks/LZMotel.WebAPI.Core/"]
RUN dotnet restore "src/services/LZMotel.Identidade.API/LZMotel.Identidade.API.csproj"
COPY . .
WORKDIR "/src/src/services/LZMotel.Identidade.API"
RUN dotnet build "LZMotel.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LZMotel.Identidade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LZMotel.Identidade.API.dll"]