FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CleanArch.Api.Template/CleanArch.Api.Template.csproj", "CleanArch.Api.Template/"]
RUN dotnet restore "CleanArch.Api.Template/CleanArch.Api.Template.csproj"
COPY . .
WORKDIR "/src/CleanArch.Api.Template"
RUN dotnet build "CleanArch.Api.Template.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CleanArch.Api.Template.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CleanArch.Api.Template.dll"]