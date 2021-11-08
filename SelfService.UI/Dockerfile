
#use the most light version of the asp.net core as a base for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Using the SDK for building the code
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
RUN dotnet build "SelfService.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelfService.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SelfService.UI.dll"]

## to build the image
#docker build -t hrui:dev .

## to create a container based on the image
# docker container run -d -p 85:80 --network hrnetwork --name hrui1 hrui:dev

## to explore the running container
#docker container exec -it web3 bash

## to delete the container & the image
#docker container rm -f web3
#docker image rm simpledockerfile1:dev