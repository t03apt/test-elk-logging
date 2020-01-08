FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ConsoleApp2/ConsoleApp2.csproj", "ConsoleApp2/"]
RUN dotnet restore "ConsoleApp2/ConsoleApp2.csproj"
COPY . .
WORKDIR "/src/ConsoleApp2"
RUN dotnet build "ConsoleApp2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ConsoleApp2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConsoleApp2.dll"]