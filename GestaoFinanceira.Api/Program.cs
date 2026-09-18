using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GestaoFinanceira.Api.Dtos;
using GestaoFinanceira.Interfaces;
using GestaoFinanceira.Models;
using GestaoFinanceira.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddSingleton<IGerenciadorFinanceiro, GerenciadorFinanceiro>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();

var transacoes = app.MapGroup("/api/transacoes").WithTags("Transações");

transacoes.MapGet("/", (IGerenciadorFinanceiro gerenciador) =>
    Results.Ok(gerenciador.ListarTodas()));

transacoes.MapGet("/saldo", (IGerenciadorFinanceiro gerenciador) =>
    Results.Ok(new { saldo = gerenciador.ObterSaldoTotal() }));

transacoes.MapGet("/categoria/{categoria}", (CategoriaEnum categoria, IGerenciadorFinanceiro gerenciador) =>
    Results.Ok(gerenciador.FiltrarPorCategoria(categoria)));

transacoes.MapGet("/periodo", (DateTime inicio, DateTime fim, IGerenciadorFinanceiro gerenciador) =>
{
    try
    {
        return Results.Ok(gerenciador.BuscarPorPeriodo(inicio, fim));
    }
    catch (ArgumentException ex)
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
});

transacoes.MapPost("/", (TransacaoRequestDto dto, IGerenciadorFinanceiro gerenciador) =>
{
    var erros = new List<ValidationResult>();
    var contexto = new ValidationContext(dto);
    if (!Validator.TryValidateObject(dto, contexto, erros, validateAllProperties: true))
        return Results.BadRequest(new { erros = erros.Select(e => e.ErrorMessage) });

    Transacao transacao = dto.Tipo switch
    {
        TipoTransacao.Receita => new Receita(dto.Descricao, dto.Valor, dto.Data, dto.Categoria),
        TipoTransacao.Despesa => new Despesas(dto.Descricao, dto.Valor, dto.Data, dto.Categoria),
        _ => throw new ArgumentException("Tipo de transação inválido.")
    };

    try
    {
        gerenciador.AdicionarTransacao(transacao);
        return Results.Created("/api/transacoes", transacao);
    }
    catch (InvalidOperationException ex)
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
});

app.Run();