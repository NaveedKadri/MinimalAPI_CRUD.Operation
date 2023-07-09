using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using MinimalAPI_CRUD.Operation.Data;
using MinimalAPI_CRUD.Operation.Model;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<PersonDb>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Get Methods
app.UseHttpsRedirection();
app.MapGet("/GetPerson",async( PersonDb db) =>
{
    var persons = await db.people.ToListAsync();
    if(persons == null )
    {
        return Results.NoContent();
    }

    return Results.Ok(persons);
});

//get by id

app.MapGet("/getpersonbyid", async (int id, PersonDb dbContext) =>
{
var person = await dbContext.people.FindAsync(id);
if (person == null)
{
return Results.NotFound();
}
return Results.Ok(person);
});

//create a new product
app.MapPost("/createperson", async (Person person, PersonDb dbContext) =>
{
var result = dbContext.people.Add(person);
await dbContext.SaveChangesAsync();
return Results.Ok(result.Entity);
});

//update the product
app.MapPut("/updateperson", async (Person person, PersonDb dbContext) =>
{
var personDetail = await dbContext.people.FindAsync(person.Id);
if (person == null)
{
        return Results.NotFound();
    }
    personDetail.Name = person.Name;
    personDetail.EmailId = person.EmailId;
    personDetail.MobileNo = person.MobileNo;

    await dbContext.SaveChangesAsync();
    return Results.Ok(personDetail);
});

//delete the product by id
app.MapDelete("/deleteperson/{id}", async (int id, PersonDb dbContext) =>
{
    var person = await dbContext.people.FindAsync(id);
    if (person == null)
    {
        return Results.NoContent();
    }
    dbContext.people.Remove(person);
    await dbContext.SaveChangesAsync();
    return Results.Ok();
});

app.Run();

internal record WeatherForecast([FromForm] DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
