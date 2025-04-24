# .NET SDK imajýný kullanýyoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001


# Build aþamasý
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm dosyasýný kopyala
COPY ["ECommerceSystem.sln", "./"]

# Projeleri kopyala
COPY ["ECommerceSystem.WebAPI/ECommerceSystem.WebAPI.csproj", "ECommerceSystem.WebAPI/"]
COPY ["ECommerceSystem.Core/ECommerceSystem.Core.csproj", "ECommerceSystem.Core/"]
COPY ["ECommerceSystem.Data/ECommerceSystem.Data.csproj", "ECommerceSystem.Data/"]
COPY ["ECommerceSystem.Service/ECommerceSystem.Service.csproj", "ECommerceSystem.Service/"]
#COPY ["ECommerceSystemWebAPI.Test/ECommerceSystemWebAPI.Test.Unit.csproj", "ECommerceSystemWebAPI.Test/"]

# NuGet restore iþlemi
RUN dotnet restore "ECommerceSystem.sln"

# Projeleri build et
COPY . . 
RUN dotnet build "ECommerceSystem.sln" -c Release -o /app/build

# Uygulama dosyalarýný çýkar
FROM build AS publish
RUN dotnet publish "ECommerceSystem.sln" -c Release -o /app/publish

# Çalýþtýrma aþamasý
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 

# Burada doðru .dll dosyasýný belirtiyoruz
ENTRYPOINT ["dotnet", "ECommerceSystem.dll"]
