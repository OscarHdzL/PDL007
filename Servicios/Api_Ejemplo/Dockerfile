#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Servicios/Api_Ejemplo/Api_Ejemplo.csproj", "Servicios/Api_Ejemplo/"]
RUN dotnet restore "Servicios/Api_Ejemplo/Api_Ejemplo.csproj"
COPY . .
WORKDIR "/src/Servicios/Api_Ejemplo"
RUN dotnet build "Api_Ejemplo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api_Ejemplo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api_Ejemplo.dll"]