using System.ComponentModel.DataAnnotations;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Api.Dtos
{
    public enum TipoTransacao
    {
        Receita,
        Despesa
    }

    public class TransacaoRequestDto
    {
        [Required]
        public TipoTransacao Tipo { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "A descrição não pode ser vazia.")]
        public string Descricao { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
        public decimal Valor { get; set; }

        public DateTime Data { get; set; } = DateTime.Now;

        public CategoriaEnum Categoria { get; set; }
    }
}