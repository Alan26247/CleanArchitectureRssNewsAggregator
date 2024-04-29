ARG PROJECT_NAME="Web"
ARG ENVIRONMENT="Production"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

ARG PROJECT_NAME
ARG ENVIRONMENT

WORKDIR /source

COPY *.sln .

COPY Core/*.csproj ./Core/
COPY Infrastructure/*.csproj ./Infrastructure/
COPY ${PROJECT_NAME}/*.csproj ./${PROJECT_NAME}/

RUN dotnet restore ${PROJECT_NAME}/${PROJECT_NAME}.csproj

COPY Core/. ./Core/
COPY Infrastructure/. ./Infrastructure/
COPY ${PROJECT_NAME}/. ./${PROJECT_NAME}/

WORKDIR /source/${PROJECT_NAME}/
RUN dotnet build ${PROJECT_NAME}.csproj -c release
RUN dotnet publish -c release -o /app --no-restore --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0

ARG PROJECT_NAME

ENV PROJECT_NAME_DLL=${PROJECT_NAME}".dll"
ENV DOTNET_gcServer=1
ENV DOTNET_GCHighMemPercent=0x8
ENV DOTNET_GCConserveMemory=7
WORKDIR /app
COPY --from=build-env /app ./

ENTRYPOINT "dotnet" $PROJECT_NAME_DLL