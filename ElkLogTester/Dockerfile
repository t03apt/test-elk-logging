FROM mcr.microsoft.com/dotnet/core/runtime:2.1-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["ElkLogTester/ElkLogTester.csproj", "ElkLogTester/"]
RUN dotnet restore "ElkLogTester/ElkLogTester.csproj"
COPY . .
WORKDIR "/src/ElkLogTester"
RUN dotnet build "ElkLogTester.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElkLogTester.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ElkLogTester.dll"]