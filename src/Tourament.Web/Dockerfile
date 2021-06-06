#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Tourament.Web/Tourament.Web.csproj", "Tourament.Web/"]
COPY ["Tournament.Core/Tournament.Core.csproj", "Tournament.Core/"]
COPY ["Tournament.Infrastructure/Tournament.Infrastructure.csproj", "Tournament.Infrastructure/"]
RUN dotnet restore "Tourament.Web/Tourament.Web.csproj"
COPY . .
WORKDIR "/src/Tourament.Web"
RUN dotnet build "Tourament.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tourament.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tourament.Web.dll"]