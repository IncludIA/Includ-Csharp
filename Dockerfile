FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY *.sln .
COPY IncludIA.Api/*.csproj IncludIA.Api/
COPY IncludIA.Application/*.csproj IncludIA.Application/
COPY IncludIA.Domain/*.csproj IncludIA.Domain/
COPY IncludIA.Infrastructure/*.csproj IncludIA.Infrastructure/
COPY IncludIA.Tests/*.csproj IncludIA.Tests/

RUN dotnet restore IncludIA.sln

COPY . .

WORKDIR "/src/IncludIA.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "IncludIA.Api.dll"]