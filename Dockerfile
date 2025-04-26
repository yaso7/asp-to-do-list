FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ToDo.sln", "./"]
COPY ["ToDo.Domain/ToDo.Domain.csproj", "ToDo.Domain/"]
COPY ["ToDo.Application/ToDo.Application.csproj", "ToDo.Application/"]
COPY ["ToDo.Infrastructure/ToDo.Infrastructure.csproj", "ToDo.Infrastructure/"]
COPY ["ToDo.WebAPI/ToDo.WebAPI.csproj", "ToDo.WebAPI/"]
RUN dotnet restore "ToDo.sln"
COPY . .
WORKDIR "/src/ToDo.WebAPI"
RUN dotnet build "ToDo.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo.WebAPI.dll"] 