FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ToDoList/ToDoList.csproj", "ToDoList/"]
COPY ["ToDoList.DAL/ToDoList.DAL.csproj", "ToDoList.DAL/"]
COPY ["ToDoList.Domain/ToDoList.Domain.csproj", "ToDoList.Domain/"]
COPY ["ToDoList.Services/ToDoList.Services.csproj", "ToDoList.Services/"]
RUN dotnet restore "ToDoList/ToDoList.csproj"
COPY . .
WORKDIR "/src/ToDoList"
RUN dotnet build "ToDoList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDoList.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoList.dll"]