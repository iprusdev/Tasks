FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["InnoShop.Products.Api/InnoShop.Products.Api.csproj", "InnoShop.Products.Api/"]
COPY ["InnoShop.Products.Application/InnoShop.Products.Application.csproj", "InnoShop.Products.Application/"]
COPY ["InnoShop.Products.Domain/InnoShop.Products.Domain.csproj", "InnoShop.Products.Domain/"]
COPY ["InnoShop.Products.Infrastructure/InnoShop.Products.Infrastructure.csproj", "InnoShop.Products.Infrastructure/"]

RUN dotnet restore "InnoShop.Products.Api/InnoShop.Products.Api.csproj"

COPY . .

WORKDIR "/src/InnoShop.Products.Api"
RUN dotnet publish "InnoShop.Products.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InnoShop.Products.Api.dll"]