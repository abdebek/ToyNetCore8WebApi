using Hangfire;
using Hangfire.MemoryStorage; // Import for In-Memory Storage
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Serialization;
using Toy.Services; // Assuming your IUserService and UserService are here

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization to use snake_case naming strategy
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
        //options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        //options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
        //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        //options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
    });

//Add Services
builder.Services.AddScoped<IUserService, UserService>();

// Add your background job class to services so it can be resolved by Hangfire
builder.Services.AddTransient<MyBackgroundJobs>(); // Or AddScoped/AddSingleton depending on its needs

// Add Swagger for testing the API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Setting up Hangfire with In-Memory Storage
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180) // Updated to current recommended
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseMemoryStorage()); // Use In-Memory storage for Hangfire

// Add the Hangfire server.
builder.Services.AddHangfireServer(options =>
{
    // Configure server options if needed, e.g.,
    // options.WorkerCount = System.Environment.ProcessorCount * 2;
    // options.Queues = new[] { "default", "critical" };
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // It's common to only expose the dashboard in development
    app.UseHangfireDashboard(); // Expose Hangfire Dashboard
}
else
{
    // In production, you might want to secure the dashboard
    // app.UseHangfireDashboard("/hangfire", new DashboardOptions
    // {
    //     Authorization = new [] { new MyHangfireDashboardAuthorizationFilter() } // Example custom auth
    // });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Schedule a job using the MyBackgroundJobs class
// The job ID "print-hi-job" is used to uniquely identify this recurring job.
RecurringJob.AddOrUpdate<MyBackgroundJobs>(
    "print-hi-job", // Give your job a unique ID
    job => job.PrintHiJob(),
    Cron.Minutely);

RecurringJob.AddOrUpdate<MyBackgroundJobs>(
"another-job-example",
job => job.AnotherJobMethod("Hello from Hangfire!"),
Cron.Minutely);


app.Run();