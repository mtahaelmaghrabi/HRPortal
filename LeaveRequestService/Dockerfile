#use the most light version of the asp.net core as a base for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Using the SDK for building the code
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src"
RUN dotnet build "LeaveRequestService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LeaveRequestService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LeaveRequestService.dll"]


## to build the image
#docker build -t mtahaelmaghrabi/leaverequestservice:latest .

## To Push the image to DockerHub
#docker image push mtahaelmaghrabi/leaverequestservice:latest