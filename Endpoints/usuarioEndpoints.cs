using Backend_Recyclo_dotnet.ViewModels;
using Backend_Recyclo_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Recyclo_dotnet.Endpoints
{
    public static class userEndpoints
    {
        
        public static void MapUsuarioEndpoints(this WebApplication app)
        {
            Random ramdom = new Random();
            int verificacao = 0;

            // Usuario by id
            app.MapGet("/usuario/{id}", async (dct8nq053j6k6dContext context,int id) =>
            {
                var usuario = await context.TbUsuarios.FirstOrDefaultAsync(x=>x.CdUsuario==id);
                if(usuario is not null)
                    return Results.Ok("Usuario encontrado!");
                else
                    return Results.NotFound("Conta nao emcontrada");
            });

            // login usuario
            app.MapGet("/login/usuario/{email}/{senha}", async 
            (dct8nq053j6k6dContext context,string email,string senha) =>
            {
                var usuario = await context.TbUsuarios.FirstOrDefaultAsync(
                    x=> x.DsEmail == email && x.CdSenha == senha);
                if(usuario is not null)
                    return Results.Ok("Usuario Encontrado");
                else
                    return Results.NotFound("Conta nao emcontrada");
            });


            // criar Usuario
            app.MapPost("/usuario/criar", 
                async (dct8nq053j6k6dContext context, [FromBody]UserViewModel modelUsuario) =>
            {
                if(modelUsuario is null)
                    return Results.BadRequest();

                try{
                    if(await context.TbUsuarios.FirstOrDefaultAsync(x =>x.CdCpf== modelUsuario.CdCpf) == null)
                    {
                        // gerando id para o usuario
                        while (true)
                        {
                            verificacao = ramdom.Next(999999999);
                            var verificaUser = await context.TbUsuarios.FirstOrDefaultAsync(x=>x.CdUsuario== verificacao);
                            if(verificaUser is null)
                                break;
                        }
                        await context.TbUsuarios.AddAsync(
                            new TbUsuario(){
                                CdCpf = modelUsuario.CdCpf,
                                NmUsuario = modelUsuario.NmUsuario,
                                CdSenha = modelUsuario.CdSenha,
                                DsEmail = modelUsuario.DsEmail,
                                CdTelefone = modelUsuario.CdTelefone,
                                CdUsuario = verificacao 
                            }
                        );
                        await context.SaveChangesAsync();
                        return Results.Ok("Ususario Cadastrado");
                    }
                    else
                        return Results.BadRequest("Usuario Existente");           
                }
                catch(Exception e){
                    return Results.BadRequest(e.ToString());
                }
                    
            });

            // Alterar Usuario
            app.MapPut("/usuario/{id}", async (dct8nq053j6k6dContext context, 
                [FromBody]UserViewModel modelUsuario, [FromRoute]int id) =>
            {
                if(modelUsuario is null)
                        return Results.BadRequest();
                
                var usuario = await context.TbUsuarios.FirstOrDefaultAsync(x=>x.CdUsuario == id);

                    if(usuario is null )
                        return Results.NotFound();

                
                try{
                    usuario.NmUsuario = modelUsuario.NmUsuario;
                    usuario.CdSenha = modelUsuario.CdSenha;
                    usuario.DsEmail = modelUsuario.DsEmail;
                    usuario.CdTelefone = modelUsuario.CdTelefone;

                    context.TbUsuarios.Update(usuario);
                    await context.SaveChangesAsync();
                    return Results.Ok();                    
                    
                }
                catch(Exception e)
                {
                    return Results.BadRequest();
                }

            });

            // deletar usuario
            app.MapDelete("/usuario/deletar/{id}", async (dct8nq053j6k6dContext context, 
                [FromRoute]int id) =>
            {
                var user = await context.TbUsuarios.FirstOrDefaultAsync(x=>x.CdUsuario ==id);
                if(user is not null)
                {
                    context.TbUsuarios.Remove(user);
                    await context.SaveChangesAsync();
                    return Results.Ok("Usuario Deletado");
                }
                else
                    return Results.NotFound("usuario nao encontrado");
            });
         
      
        }
    }
}