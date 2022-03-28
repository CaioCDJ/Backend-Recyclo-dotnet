using Backend_Recyclo_dotnet.ViewModels;
using Backend_Recyclo_dotnet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Recyclo_dotnet.Endpoints
{
    public  static class empresaEndpoints
    {
        public static void MapEmpresaEndpoints(this WebApplication app)
        {
            Random ramdom = new Random();
            int verificacao = 0;

            // empresa by id
            app.MapGet("/empresa/{id}", async (dct8nq053j6k6dContext context,int id) =>
            {
                var empresa = await context.TbEmpresas.FirstOrDefaultAsync(x=>x.CdEmpresa==id);
                if(empresa is not null)
                    return Results.Ok("Conta encontrado!");
                else
                    return Results.NotFound("Conta nao emcontrada");
            });

            // login empresa
            app.MapGet("/login/empresa/{email}/{senha}", async 
            (dct8nq053j6k6dContext context,string email,string senha) =>
            {
                var usuario = await context.TbEmpresas.FirstOrDefaultAsync(
                    x=> x.NmEmail == email && x.CdSenhaEmpresa == senha);
                if(usuario is not null)
                    return Results.Ok("Conta Encontrado");
                else
                    return Results.NotFound("Conta nao emcontrada");
            });


            // criar empresa
            app.MapPost("/empresa/criar", 
                async (dct8nq053j6k6dContext context, [FromBody]EmpresaViewModel empresaView) =>
            {
                if(empresaView is null)
                    return Results.BadRequest();

                try{

                    if(await context.TbEmpresas.FirstOrDefaultAsync(x =>x.CdCnpj== empresaView.CdCnpj) == null)
                    {
                        // gerando id para o usuario
                        while (true)
                        {
                            verificacao = ramdom.Next(999999999);
                            var verificaUser = await context.TbEmpresas.FirstOrDefaultAsync(x=>x.CdEmpresa== verificacao);
                            if(verificaUser is null)
                                break;
                        }
                        await context.TbEmpresas.AddAsync(
                            new TbEmpresa(){
                                CdCnpj = empresaView.CdCnpj,
                                NmEmpresa = empresaView.NmEmpresa,
                                CdSenhaEmpresa = empresaView.CdSenhaEmpresa,
                                NmEmail= empresaView.NmEmail,
                                CdTelefone = empresaView.CdTelefone,
                                CdEmpresa = verificacao 
                            }
                        );
                        await context.SaveChangesAsync();
                        return Results.Ok("Empresa Cadastrada ");
                    }
                    else
                        return Results.BadRequest("Empresa Existente");           
                }
                catch(Exception e){
                    return Results.BadRequest();
                }
                    
            });

            // Alterar empresa
            app.MapPut("/empresa/{id}", async (dct8nq053j6k6dContext context, 
                [FromBody]EmpresaViewModel empresaView, [FromRoute]int id) =>
            {
                if(empresaView is null)
                        return Results.BadRequest();
                
                var empresa = await context.TbEmpresas.FirstOrDefaultAsync(x=>x.CdEmpresa == id);

                if(empresa is null )
                    return Results.NotFound();

                try{
                    empresa.NmEmpresa = empresaView.NmEmpresa;
                    empresa.CdSenhaEmpresa = empresaView.CdSenhaEmpresa;
                    empresa.NmEmail = empresaView.NmEmail;
                    empresa.CdTelefone = empresaView.CdTelefone;

                    context.TbEmpresas.Update(empresa);
                    await context.SaveChangesAsync();
                    return Results.Ok();                    
                }
                catch(Exception e)
                {
                    return Results.BadRequest();
                }

            });

            // deletar empresa
            app.MapDelete("/empresa/deletar/{id}", async (dct8nq053j6k6dContext context, 
                [FromRoute]int id) =>
            {
                var empresa = await context.TbEmpresas.FirstOrDefaultAsync(x=>x.CdEmpresa ==id);
                if(empresa is not null)
                {
                    context.TbEmpresas.Remove(empresa);
                    await context.SaveChangesAsync();
                    return Results.Ok("Usuario Deletado");
                }
                else
                    return Results.NotFound("usuario nao encontrado");
            });           
        }
    }
}