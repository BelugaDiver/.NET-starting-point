using SingletonRepository;
using SingletonRepository.DataLayer.Interfaces;
using SingletonRepository.DataLayer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Inject dependencies necessary for the graph schema.
builder.Services.AddSingleton(
    provider => StudentSingletonRepository.GetSingleton(SampleData.GetStudents())
);

builder.Services.AddSingleton(
    provider => CourseSingletonRepository.GetSingleton(SampleData.GetCourses())
);

builder.Services.AddScoped<IStudentRepository, StudentSingletonRepository>(
    provider => provider.GetRequiredService<StudentSingletonRepository>()
);

builder.Services.AddScoped<ICourseRepository, CourseSingletonRepository>(
    provider => provider.GetRequiredService<CourseSingletonRepository>()
);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();
