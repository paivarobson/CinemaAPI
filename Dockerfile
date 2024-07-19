FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS build
WORKDIR /app

COPY ["CinemaApi.csproj", "Cinema/"]
RUN dotnet restore "Cinema/CinemaApi.csproj"

COPY . .
WORKDIR "/src/Cinema"
RUN dotnet build "CinemaApi.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /app
COPY --from=base /app/build .

EXPOSE 80
EXPOSE 443

ENTRYPOINT [ "dotnet", "CinemaApi.dll" ]