FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY CinemaApi.csproj .
RUN ls -l
RUN dotnet restore CinemaApi.csproj
COPY . .
RUN dotnet build CinemaApi.csproj -c Release -o /app/build

EXPOSE 80
EXPOSE 443

FROM build AS publish
RUN dotnet publish CinemaApi.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "CinemaApi.dll" ]
