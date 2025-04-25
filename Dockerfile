# .NET SDK imaj�n� kullan�yoruz
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

# Build a�amas�
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# ��z�m dosyas�n� kopyala
COPY ["ECommerceSystem.API.sln", "./"]

# Projeleri kopyala
COPY ["ECommerceSystem/ECommerceSystem.csproj", "ECommerceSystem/"]
COPY ["ECommerceSystem.Core/ECommerceSystem.Core.csproj", "ECommerceSystem.Core/"]
COPY ["ECommerceSystem.Data/ECommerceSystem.Data.csproj", "ECommerceSystem.Data/"]
COPY ["ECommerceSystem.Service/ECommerceSystem.Service.csproj", "ECommerceSystem.Service/"]

# NuGet restore i�lemi
RUN dotnet restore "ECommerceSystem.API.sln"

# Projeleri build et
COPY . . 
RUN dotnet build "ECommerceSystem.API.sln" -c Release -o /app/build

# Uygulama dosyalar�n� ��kar
FROM build AS publish
RUN dotnet publish "ECommerceSystem.API.sln" -c Release -o /app/publish

# �al��t�rma a�amas�
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish . 

# Burada do�ru .dll dosyas�n� belirtiyoruz
ENTRYPOINT ["dotnet", "ECommerceSystem.dll"]

