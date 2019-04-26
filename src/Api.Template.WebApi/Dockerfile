FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Api.Template.WebApi/Api.Template.WebApi.csproj", "Api.Template.WebApi/"]
COPY ["Api.Template.Infrastructure/Api.Template.Infrastructure.csproj", "Api.Template.Infrastructure/"]
COPY ["Api.Template.Mock.Infrastructure/Api.Common.Mock.Infrastructure.csproj", "Api.Template.Mock.Infrastructure/"]
COPY ["Api.Template.ApplicationService/Api.Template.ApplicationService.csproj", "Api.Template.ApplicationService/"]
COPY ["Api.Template.Domain/Api.Template.Domain.csproj", "Api.Template.Domain/"]
COPY ["Api.Common.Cqrs.Core/Api.Common.Cqrs.Core.csproj", "Api.Common.Cqrs.Core/"]
COPY ["Api.Common.Repository.Contracts.Core/Api.Common.Repository.Contracts.Core.csproj", "Api.Common.Repository.Contracts.Core/"]
COPY ["Api.Template.DbEFCore/Api.Template.DbEFCore.csproj", "Api.Template.DbEFCore/"]
COPY ["Api.Common.Repository.EFCore/Api.Common.Repository.EFCore.csproj", "Api.Common.Repository.EFCore/"]
COPY ["Api.Common.WebServer/Api.Common.WebServer.csproj", "Api.Common.WebServer/"]
RUN dotnet restore "Api.Template.WebApi/Api.Template.WebApi.csproj"
COPY . .
WORKDIR "/src/Api.Template.WebApi"
RUN dotnet build "Api.Template.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Api.Template.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Api.Template.WebApi.dll"]