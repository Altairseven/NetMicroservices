FROM mcr.microsoft.com/dotnet/sdk AS build-env

WORKDIR /app
RUN mkdir PlatformModels
COPY  ./PlatformModels ./PlatformModels
RUN dotnet restore PlatformModels


RUN mkdir PlatformService
COPY  ./PlatformService ./PlatformService
RUN dotnet restore PlatformService


RUN dotnet publish PlatformService -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "PlatformService.dll"]