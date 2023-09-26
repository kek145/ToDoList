FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY ToDoList.BL.Mediator/*csproj ToDoList.BL.Mediator/
COPY ToDoList.BL.Services/*csproj ToDoList.BL.Services/
COPY ToDoList.DAL.Core/*csproj ToDoList.DAL.Core/
COPY ToDoList.DAL.Repositories/*csproj ToDoList.DAL.Repositories/
COPY ToDoList.Domain.Contracts/*csproj ToDoList.Domain.Contracts/
COPY ToDoList.Domain.Entities/*csproj ToDoList.Domain.Entities/
COPY ToDoList.Security/*csproj ToDoList.Security/
COPY ToDoList.Api/*csproj ToDoList.Api/

RUN dotnet restore ToDoList.BL.Mediator/
RUN dotnet restore ToDoList.BL.Services/
RUN dotnet restore ToDoList.DAL.Core/
RUN dotnet restore ToDoList.DAL.Repositories/
RUN dotnet restore ToDoList.Domain.Contracts/
RUN dotnet restore ToDoList.Domain.Entities/
RUN dotnet restore ToDoList.Security/
RUN dotnet restore ToDoList.Api/

COPY . .

RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /publish
COPY --from=build-env /publish .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT ["dotnet", "ToDoList.Api.dll"]