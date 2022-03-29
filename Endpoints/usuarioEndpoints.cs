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
                    if(await context.TbUsuarios.FirstOrDefaultAsync(x =>x.CdCpf == modelUsuario.CdCpf 
                        || x.DsEmail == modelUsuario.DsEmail) == null)
                    {
                        await context.TbUsuarios.AddAsync(
                            new TbUsuario(){
                                CdCpf = modelUsuario.CdCpf,
                                NmUsuario = modelUsuario.NmUsuario,
                                CdSenha = modelUsuario.CdSenha,
                                DsEmail = modelUsuario.DsEmail,
                                CdTelefone = modelUsuario.CdTelefone,
                                CdUsuario = ID.newID(ID.Tabela.usuario)
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

            // DENUNCIAS         

            // denuncias realizadas por um usuario 
            app.MapGet("/denuncias/{email}/{senha}",async 
            (dct8nq053j6k6dContext context, string email,string senha) =>
            {
                var usuario = await context.TbUsuarios.FirstOrDefaultAsync(
                    x=>x.DsEmail == email && x.CdSenha == senha);
                
                if(usuario is null)
                    return Results.NotFound("Conta nao encontrado");

                var denuncias = await context.TbDenuncia.FirstOrDefaultAsync(
                    x=>x.FkCdUsuario ==  usuario.CdUsuario);
                if(denuncias is not null)
                    return Results.Ok(denuncias);
                else
                    return Results.NotFound("Esta conta nao possui denuncias");
            });

            // criar denuncia
            app.MapPost("/denuncias/criar/{email}/{senha}", async(
                dct8nq053j6k6dContext context,[FromBody]DenunciaViewModel denunciaView, [FromRoute]string email, [FromRoute]string senha) => {
                    
                    var usuario = await context.TbUsuarios.FirstOrDefaultAsync(
                        x=>x.DsEmail ==email && x.CdSenha == senha);
                    
                    if(usuario is null || denunciaView is null)
                        return Results.BadRequest();

                    try{
                        await context.TbDenuncia.AddAsync(
                            new TbDenuncium(){
                                DsComentario = denunciaView.DsComentario,
                                DtDenuncia = denunciaView.DtDenuncia,
                                NmLogradouro = denunciaView.NmLogradouro,
                                CdDenuncia = ID.newID(ID.Tabela.denuncia),
                                FkCdUsuario = usuario.CdUsuario
                        });
                        await context.SaveChangesAsync();
                        return Results.Ok();

                    }
                    catch
                    {
                        return Results.BadRequest();
                    }
            });
            
            // alterar de denuncia
            app.MapPut("/denuncias/alterar/{id}/{dsComentario}",async (
                dct8nq053j6k6dContext context, [FromRoute]int id, [FromRoute]string dsComentario) => {
                
                var denuncia = await context.TbDenuncia.FirstOrDefaultAsync(
                    x=>x.CdDenuncia == id);

                if(denuncia is null || String.IsNullOrEmpty(dsComentario))
                    return Results.BadRequest();

                try
                {
                    denuncia.DsComentario = dsComentario;
                    await context.TbDenuncia.AddAsync(denuncia);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch 
                {
                    return Results.BadRequest();
                }
            });


            // deletar denuncias
            app.MapDelete("/denucias/deletar/{id}", async([FromServices]dct8nq053j6k6dContext context,[FromRoute]int id)=>
            {
                var denuncia = await context.TbDenuncia.FirstOrDefaultAsync(
                    x=>x.CdDenuncia == id);
                if(denuncia is null)
                    return Results.NotFound();
                try{
                    context.TbDenuncia.Remove(denuncia);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch{return Results.BadRequest();}
            });

            app.MapGet("/denuncias", async (dct8nq053j6k6dContext context) =>
                await context.TbDenuncia.ToListAsync());      
        }
    }
}