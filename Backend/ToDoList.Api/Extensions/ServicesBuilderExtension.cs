namespace ToDoList.Api.Extensions;

public static class ServicesBuilderExtension
{
    public static IServiceCollection AddScopedService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<ITaskRepository, TaskRepository>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ITokenRepository, TokenRepository>();

        return serviceCollection;
    }

    public static IServiceCollection AddTransientService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ITaskService, TaskService>();
        serviceCollection.AddTransient<ITokenService, TokenService>();
        serviceCollection.AddTransient<IAccountService, AccountService>();
        serviceCollection.AddTransient<IRegistrationService, RegistrationService>();
        serviceCollection.AddTransient<IAuthenticationService, AuthenticationService>();
        serviceCollection.AddTransient<IValidator<TaskRequest>, TaskRequestValidator>();
        serviceCollection.AddTransient<IRequestHandler<SaveTokenCommand>, SaveTokenHandler>();
        serviceCollection.AddTransient<IRequestHandler<DeleteUserCommand, bool>, DeleteUserHandler>();
        serviceCollection.AddTransient<IRequestHandler<UpdateTaskCommand, bool>, UpdateTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<DeleteTaskCommand, bool>, DeleteTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<UpdateEmailCommand, bool>, UpdateEmailHandler>();
        serviceCollection.AddTransient<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
        serviceCollection.AddTransient<IRequestHandler<DeleteTokenCommand, bool>, DeleteTokenHandler>();
        serviceCollection.AddTransient<IRequestHandler<CompleteTaskCommand, bool>, CompleteTaskHandler>();
        serviceCollection.AddTransient<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
        serviceCollection.AddTransient<IRequestHandler<UpdatePasswordCommand, bool>, UpdatePasswordHandler>();
        serviceCollection.AddTransient<IRequestHandler<UpdateFullNameCommand, bool>, UpdateFullNameHandler>();
        serviceCollection.AddTransient<IRequestHandler<ValidationTokenCommand, bool>, ValidationTokenHandler>();
        serviceCollection.AddTransient<IRequestHandler<CreateUserCommand, GetUserResponse>, CreateUserHandler>();
        serviceCollection.AddTransient<IRequestHandler<CreateTaskCommand, GetTaskResponse>, CreateTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetTaskByIdQuery, GetTaskResponse>, GetTaskByIdHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetUserInfoQuery, GetUserInfoResponse>, GetUserInfoHandler>();
        serviceCollection.AddTransient<IRequestHandler<RefreshTokenCommand, GetUserResponse>, RefreshTokenHandler>();
        serviceCollection.AddTransient<IRequestHandler<AuthenticationCommand, GetUserResponse>, AuthenticationHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetUserFullNameQuery, GetUserFullNameResponse>, GetUserFullNameHandler>();
        serviceCollection.AddTransient<IRequestHandler<SearchTaskQuery, PaginationResponse<GetTaskResponse>>, SearchTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllFailedTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllFailedTaskHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllTaskByPriorityQuery, PaginationResponse<GetTaskResponse>>, GetTaskByPriorityHandler>();
        serviceCollection.AddTransient<IRequestHandler<GetAllCompletedTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllCompletedTaskHandler>();

        return serviceCollection;
    }
}