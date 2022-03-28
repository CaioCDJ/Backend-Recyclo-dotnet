using Backend_Recyclo_dotnet.Models;
using Backend_Recyclo_dotnet.Endpoints;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<dct8nq053j6k6dContext>();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapUsuarioEndpoints();
app.MapEmpresaEndpoints();

app.UseHttpsRedirection();
app.Run();

/* Engenharia reversa no banco de dados
    dotnet ef dbcontext scaffold "Host=ec2-54-197-100-79.compute-1.amazonaws.com;Database=dct8nq053j6k6d;Username=zozpujqxeubpha;Password=a61992ba0d8f928b5f77c6b69943eb716ff3de0bc8188675a76c84727a44fa9f" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
;*/