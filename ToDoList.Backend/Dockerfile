FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

COPY ToDoList.Backend.Infrastructure/*csproj ToDoList.Backend.Infrastructure/
COPY ToDoList.Backend.Application/*csproj ToDoList.Backend.Application/
COPY ToDoList.Backend.Domain/*csproj ToDoList.Backend.Domain/
COPY ToDoList.Backend.Api/*csproj ToDoList.Backend.Api/

RUN dotnet restore BlogAPI.DAL/
RUN dotnet restore BlogAPI.BL/
RUN dotnet restore BlogAPI.Domain/
RUN dotnet restore BlogAPI.Security/
RUN dotnet restore ToDoList.Backend.Api/

COPY ToDoList.Backend.Infrastructure/. ToDoList.Backend.Infrastructure/
COPY ToDoList.Backend.Application/. ToDoList.Backend.Application/
COPY ToDoList.Backend.Domain/. ToDoList.Backend.Domain/

FROM build-env AS build-app
WORKDIR /src/ToDoList.Backend.Api

COPY ToDoList.Backend.Api/. .

RUN dotnet build -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-app /app/publish .

EXPOSE 80

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "ToDoList.Backend.Api.dll"]