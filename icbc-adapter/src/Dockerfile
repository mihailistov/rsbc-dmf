#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
ARG BUILD_ID
ARG BUILD_VERSION
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILD_ID
ARG BUILD_VERSION
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_ID
ARG BUILD_VERSION
COPY . .
RUN dotnet restore ./icbc-adapter/src/Pssg.Dmf.IcbcAdapter 

FROM build AS publish
ARG BUILD_ID
ARG BUILD_VERSION
RUN dotnet publish ./icbc-adapter/src/Pssg.Dmf.IcbcAdapter /p:Version="$BUILD_VERSION" /p:InformationalVersion="$BUILD_ID" -c Release -o /app/publish

FROM base AS final
ARG BUILD_ID
ARG BUILD_VERSION
WORKDIR /app
COPY --from=publish /app/publish .

# RUN apt-get update -yq \
#    && apt-get install curl -yq \
#	&& curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v vs2019 -l /app/vsdbg

EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Pssg.Dmf.IcbcAdapter.dll"]