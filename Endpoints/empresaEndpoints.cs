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

                    if(await context.TbEmpresas.FirstOrDefaultAsync(x =>x.CdCnpj == empresaView.CdCnpj 
                        || x.NmEmail == empresaView.NmEmail) == null)
                    {
                        await context.TbEmpresas.AddAsync(
                            new TbEmpresa(){
                                CdCnpj = empresaView.CdCnpj,
                                NmEmpresa = empresaView.NmEmpresa,
                                CdSenhaEmpresa = empresaView.CdSenhaEmpresa,
                                NmEmail= empresaView.NmEmail,
                                CdTelefone = empresaView.CdTelefone,
                                CdEmpresa = ID.newID(ID.Tabela.empresa) 
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
                    return Results.Ok("Conta Deletado");
                }
                else
                    return Results.NotFound("Conta nao encontrado");
            });           

            // PONTOS DE COLETA

            // pontos
            app.MapGet("/pontos", async (dct8nq053j6k6dContext context)=>
                await context.TbPontoColeta.ToListAsync());
            
            // pontos de uma conta
            app.MapGet("/pontos/{email}/{senha}", async (
                dct8nq053j6k6dContext context, string email, string senha) => {
                
                var empresa = await context.TbEmpresas.FirstOrDefaultAsync(
                    x=>x.NmEmail == email && x.CdSenhaEmpresa == senha);
                
                if(empresa is null)
                    return Results.NotFound("Conta nao encontrada");
                
                var pontos = await context.TbPontoColeta.FirstOrDefaultAsync(
                    x=>x.FkCdEmpresa == empresa.CdEmpresa);
                
                if(pontos is null)
                    return Results.NoContent();
                else
                    return Results.Ok(pontos);
            });

            // criar ponto 
            app.MapPost("/pontos/criar/{id}", async (
                dct8nq053j6k6dContext context,[FromBody]PontosViewModel pontosView, int id) => {
                
                var empresa = await context.TbEmpresas.FirstOrDefaultAsync(
                    x=>x.CdEmpresa == id);
            
                if(pontosView is null || empresa is null)
                    return Results.BadRequest();
                
                try{
                    await context.TbPontoColeta.AddAsync(
                        new TbPontoColetum(){
                            CdLatitudePonto = pontosView.CdLatitudePonto,
                            CdLongitudePonto = pontosView.CdLongitudePonto,
                            NmLogradouro = pontosView.NmLogradouro,
                            NmPonto = pontosView.NmPonto,
                            FkCdEmpresa = ID.newID(ID.Tabela.ponto)
                        }
                    );
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch
                {
                    return Results.BadRequest();
                }
            });
            
            // alterar ponto
            app.MapPut("/pontos/alterar/{id}/{nome}/{logradouro}", async (
                dct8nq053j6k6dContext context, int id, string nome, string logradouro)=>{
    
                var ponto = await context.TbPontoColeta.FirstOrDefaultAsync(
                    x=>x.CdPontoColeta == id);

                if(ponto is null)
                    return Results.NotFound();

                try
                {
                    ponto.NmLogradouro = logradouro;
                    ponto.NmPonto = nome;
                    context.TbPontoColeta.Update(ponto);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            });
            
            // deletar ponto
            app.MapDelete("/pontos/deletar/{id}", async (dct8nq053j6k6dContext context, int id) => {               
                var ponto = await context.TbPontoColeta.FirstOrDefaultAsync(
                    x=>x.CdPontoColeta == id);

                if(ponto is null)
                    return Results.NotFound();

                try{
                    context.TbPontoColeta.Remove(ponto);
                    await context.SaveChangesAsync();
                    return Results.Ok();
                }
                catch{return Results.BadRequest();}
            });

        }
    }
}