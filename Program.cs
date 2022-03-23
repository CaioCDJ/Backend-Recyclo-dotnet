using Backend_Recyclo_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Backend_Recyclo_dotnet.Controller;


Random random = new Random();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<dct8nq053j6k6dContext>();
var app = builder.Build();


// Informacoes usuario
app.MapGet("/usuario/{email}/{senha}", async (string email, string senha,dct8nq053j6k6dContext context) =>
    await context.TbUsuarios.FirstOrDefaultAsync( a => a.DsEmail == email && a.CdSenha == senha));

// cadastro Usuario
app.MapPost("/usuario/criar/{nome}/{email}/{telefone}/{cpf}/{senha}", async (
    string nome,string email,string telefone,string cpf, string senha, dct8nq053j6k6dContext context) =>
{
    TbUsuario usuario = new TbUsuario();
                
    usuario.NmUsuario = nome;
    usuario.DsEmail = email;
    usuario.CdCpf = cpf;
    usuario.CdTelefone = telefone;
    usuario.CdSenha = senha;

    usuario.CdUsuario = random.Next(999999);
    
    context.TbUsuarios.Add(usuario);
    await context.SaveChangesAsync();
    return;
});

// delete
app.MapDelete("/usuario/deletar/{id}", async (int id, dct8nq053j6k6dContext context) =>
{
    var usuario = await context.TbUsuarios.FirstOrDefaultAsync(a => a.CdUsuario==id);
    if(usuario!=null)
    {
        context.TbUsuarios.Remove(usuario);
        await context.SaveChangesAsync();
    }
    return;
});


// Informacoes empresa
app.MapGet("/empresa/{email}/{senha}", async (string email, string senha,dct8nq053j6k6dContext context) =>
    await context.TbEmpresas.FirstOrDefaultAsync( a => a.NmEmail == email && a.CdSenhaEmpresa == senha));


app.Run();

/* Engenharia reversa no banco de dados
    dotnet ef dbcontext scaffold "Host=ec2-54-197-100-79.compute-1.amazonaws.com;Database=dct8nq053j6k6d;Username=zozpujqxeubpha;Password=a61992ba0d8f928b5f77c6b69943eb716ff3de0bc8188675a76c84727a44fa9f" Npgsql.EntityFrameworkCore.PostgreSQL -o Models
*/