#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY ["/API/API.csproj", "/API/"]
COPY ["/Application/Application.csproj", "/Application/"]
COPY ["/Domain/Domain.csproj", "/Domain/"]
COPY ["/Infra/Infra.csproj", "/Infra/"]
RUN dotnet restore "/API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API.csproj" -c Release -o /app/publish

FROM base AS final
# create a new user and change directory ownership
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app

# impersonate into the new user
USER dotnetuser
WORKDIR /app

COPY --from=publish /app/publish .
#RUN dotnet tool install --global dotnet-ef
#ENV PATH "$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "API.dll"]
