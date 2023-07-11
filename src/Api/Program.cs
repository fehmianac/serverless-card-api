using System.Reflection;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Api.Extensions;
using Api.Infrastructure.Context;
using Domain.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddScoped<IApiContext, ApiContext>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddDefaultAWSOptions(new AWSOptions
{
    Profile = "serverless",
    Region = RegionEndpoint.EUCentral1
});

builder.Services.AddScoped<IApiContext, ApiContext>();
builder.Services.AddScoped<IAttributeRepository, AttributeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpointsCore(AppDomain.CurrentDomain.GetAssemblies());

app.Run();

static IEnumerable<Assembly> GetAssembly()
{
    yield return typeof(Program).Assembly;
}