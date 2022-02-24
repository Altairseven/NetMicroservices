# BUILD
FROM mcr.microsoft.com/dotnet/sdk AS build-env

WORKDIR /app
RUN mkdir CommandModels
COPY  ./CommandModels ./CommandModels
RUN dotnet restore CommandModels


RUN mkdir CommandService
COPY  ./CommandService ./CommandService
RUN dotnet restore CommandService


RUN dotnet publish CommandService -c Release -o out

# RUN
FROM mcr.microsoft.com/dotnet/aspnet

WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "CommandService.dll"]