using LocWarning.Data;
using LocWarning.Model;
using LocWarning.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Database
builder.Services.Configure<LocWarningDatabaseSettings>(builder.Configuration.GetSection("LocWarningDataBaseSettings"));

//Services
builder.Services.AddSingleton<LocationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}





app.UseHttpsRedirection();


app.MapGet("/", () => "LocWarnings!!!");

app.MapGet("/api/locations", async (LocationService locationService) => await locationService.Get());

app.MapPost("/api/locations", async (LocationService locationService, Location location) =>
{
    await locationService.Create(location);
    return Results.Ok();
});

app.MapGet("/api/locations/{id}", async (LocationService locationService, string id) =>
{
    var location = await locationService.Get(id);
    return location is null ? Results.NotFound() : Results.Ok(location);
});

app.MapPut("/api/locations/{id}", async (LocationService locationService, Location updatedLocation, string id) =>
{
    var location = await locationService.Get(id);
    if (location is null)
        return Results.NotFound();

    updatedLocation.Id = location.Id;
    await locationService.Update(id, updatedLocation);
    return Results.Ok();
});

app.MapDelete("/api/locations/{id}", async (LocationService locationService, string id) =>
{
    var location = await locationService.Get(id);
    if (location is null)
        return Results.NotFound();

    locationService.Remove(location.Id);
    return Results.NoContent();
});


app.Run();

