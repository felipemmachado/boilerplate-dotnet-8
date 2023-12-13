#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
EXPOSE 80
EXPOSE 443
WORKDIR /
COPY ["/API/API.csproj", "/API/"]
COPY ["/Application/Application.csproj", "/Application/"]
COPY ["/Domain/Domain.csproj", "/Domain/"]
COPY ["/Infra/Infra.csproj", "/Infra/"]
RUN dotnet restore "/API/API.csproj"
COPY . .
WORKDIR "/API"
RUN dotnet build "API.csproj" -c Release -o /app/build
RUN dotnet publish "API.csproj" -c Release -o /app/publish
WORKDIR /app
RUN cp -R /app/publish/* .
RUN dotnet tool install --global dotnet-ef
ENV PATH "$PATH:/root/.dotnet/tools"
ENTRYPOINT ["dotnet", "API.dll"]