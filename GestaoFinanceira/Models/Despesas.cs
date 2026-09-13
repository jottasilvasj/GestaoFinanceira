using System;

namespace GestaoFinanceira.Models
{
    public class Despesas : Transacao
    {
        public Despesas() { }

        public Despesas(string descricao, decimal valor, DateTime data, CategoriaEnum categoria)
            : base(descricao, valor, data, categoria)
        {
        }

        public override decimal CalcularImpactoSaldo()
        {
            return -Valor;
        }
    }
}