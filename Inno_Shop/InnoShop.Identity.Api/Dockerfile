FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InnoShop.Identity.Api/InnoShop.Identity.Api.csproj", "InnoShop.Identity.Api/"]
COPY ["InnoShop.Identity.Application/InnoShop.Identity.Application.csproj", "InnoShop.Identity.Application/"]
COPY ["InnoShop.Identity.Domain/InnoShop.Identity.Domain.csproj", "InnoShop.Identity.Domain/"]
COPY ["InnoShop.Identity.Infrastructure/InnoShop.Identity.Infrastructure.csproj", "InnoShop.Identity.Infrastructure/"]


RUN dotnet restore "InnoShop.Identity.Api/InnoShop.Identity.Api.csproj"


COPY . .

WORKDIR "/src/InnoShop.Identity.Api"
RUN dotnet publish "InnoShop.Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InnoShop.Identity.Api.dll"]