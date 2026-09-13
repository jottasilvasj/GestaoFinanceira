using System;
using System.Text.Json.Serialization;

namespace GestaoFinanceira.Models
{
    [JsonDerivedType(typeof(Receita), typeDiscriminator: "receita")]
    [JsonDerivedType(typeof(Despesas), typeDiscriminator: "despesa")]
    public abstract class Transacao
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public CategoriaEnum Categoria { get; set; }

        public Transacao() { }

        public Transacao(string descricao, decimal valor, DateTime data, CategoriaEnum categoria)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
        }

        public abstract decimal CalcularImpactoSaldo();
    }
}