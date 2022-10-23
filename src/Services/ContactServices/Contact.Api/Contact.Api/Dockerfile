#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/ContactServices/Contact.Api/Contact.Api/Contact.Api.csproj", "src/Services/ContactServices/Contact.Api/Contact.Api/"]
COPY ["src/Services/ContactServices/Contact.Application/Contact.Application/Contact.Application.csproj", "src/Services/ContactServices/Contact.Application/Contact.Application/"]
COPY ["src/Services/ContactServices/Contact.Domain/Contact.Domain/Contact.Domain.csproj", "src/Services/ContactServices/Contact.Domain/Contact.Domain/"]
COPY ["src/BuildingBlocks/Contracts/Contracts.csproj", "src/BuildingBlocks/Contracts/"]
COPY ["src/Services/ContactServices/Contact.Infrastructure/Contact.Infrastructure/Contact.Infrastructure.csproj", "src/Services/ContactServices/Contact.Infrastructure/Contact.Infrastructure/"]
RUN dotnet restore "src/Services/ContactServices/Contact.Api/Contact.Api/Contact.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/ContactServices/Contact.Api/Contact.Api"
RUN dotnet build "Contact.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact.Api.dll"]