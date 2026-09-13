using System;

namespace GestaoFinanceira.Models
{
    public class Receita : Transacao
    {
        public Receita() { }

        public Receita(string descricao, decimal valor, DateTime data, CategoriaEnum categoria)
            : base(descricao, valor, data, categoria)
        {
        }

        public override decimal CalcularImpactoSaldo()
        {
            return Valor;
        }
    }
}