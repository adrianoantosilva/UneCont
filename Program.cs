using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Habilita arquivos estáticos (wwwroot/index.html)
builder.Services.AddDirectoryBrowser();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles(); // carrega index.html automaticamente
app.UseStaticFiles();

// Lista em memória
var notas = new List<NotaFiscal>();

// POST /notas
app.MapPost("/notas", (NotaFiscalInput input) =>
{
    var novaNota = new NotaFiscal(
        input.Numero,
        input.Cliente,
        input.Valor,
        input.DataEmissao,
        DateTime.Now
    );
    notas.Add(novaNota);
    return Results.Ok(novaNota);

});


// app.MapPost("/notas", ([Microsoft.AspNetCore.Mvc.FromBody] NotaFiscalInput input) =>
// {
//     Debugger.Break();
//     var novaNota = new NotaFiscal(
//         input.Numero,
//         input.Cliente,
//         input.Valor,
//         input.DataEmissao,
//         DateTime.Now
//     );
//     notas.Add(novaNota);
//     return Results.Ok(novaNota);
// });


// GET /notas
app.MapGet("/notas", () => notas);

app.Run();

// -------------------
// Models
record NotaFiscal(int Numero, string Cliente, decimal Valor, DateTime DataEmissao, DateTime DataCadastro);
record NotaFiscalInput(int Numero, string Cliente, decimal Valor, DateTime DataEmissao);
