var builder = WebApplication.CreateBuilder(args);

var secretKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWTConfiguration:JWT_ACCESS_SECRET").Value!);
var issuer = builder.Configuration.GetSection("JWTConfiguration:Issuer").Value;
var audience = builder.Configuration.GetSection("JWTConfiguration:Audience").Value;

var tokenValidationParameter = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidIssuer = issuer,
    ValidateAudience = true,
    ValidAudience = audience,
    ValidateLifetime = true,
    ClockSkew = TimeSpan.Zero,
    RequireExpirationTime = true,
    IssuerSigningKey = new SymmetricSecurityKey(secretKey)
};

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();

builder.Services.AddTransient<ITaskService, TaskService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IValidator<TaskRequest>, TaskRequestValidator>();
builder.Services.AddTransient<IRequestHandler<SaveTokenCommand>, SaveTokenHandler>();
builder.Services.AddTransient<IRequestHandler<UpdateTaskCommand, bool>, UpdateTaskHandler>();
builder.Services.AddTransient<IRequestHandler<DeleteTaskCommand, bool>, DeleteTaskHandler>();
builder.Services.AddTransient<IValidator<RegistrationRequest>, RegistrationRequestValidator>();
builder.Services.AddTransient<IRequestHandler<CompleteTaskCommand, bool>, CompleteTaskHandler>();
builder.Services.AddTransient<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
builder.Services.AddTransient<IRequestHandler<ValidationTokenCommand, bool>, ValidationTokenHandler>();
builder.Services.AddTransient<IRequestHandler<CreateUserCommand, GetUserResponse>, CreateUserHandler>();
builder.Services.AddTransient<IRequestHandler<CreateTaskCommand, GetTaskResponse>, CreateTaskHandler>();
builder.Services.AddTransient<IRequestHandler<GetTaskByIdQuery, GetTaskResponse>, GetTaskByIdHandler>();
builder.Services.AddTransient<IRequestHandler<AuthenticationCommand, GetUserResponse>, AuthenticationHandler>();
builder.Services.AddTransient<IRequestHandler<SearchTaskQuery, PaginationResponse<GetTaskResponse>>, SearchTaskHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllTaskHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllFailedTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllFailedTaskHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllTaskByPriorityQuery, PaginationResponse<GetTaskResponse>>, GetTaskByPriorityHandler>();
builder.Services.AddTransient<IRequestHandler<GetAllCompletedTaskQuery, PaginationResponse<GetTaskResponse>>, GetAllCompletedTaskHandler>();

builder.Services.AddSingleton(tokenValidationParameter);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"))
        .UseSnakeCaseNamingConvention();
});

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.Always;
    options.Secure = CookieSecurePolicy.Always;
});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme(\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.SaveToken = true;
    jwt.TokenValidationParameters = tokenValidationParameter;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEnd", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("FrontEnd");

app.MapControllers();

app.AddGlobalErrorHandler();

app.Run();