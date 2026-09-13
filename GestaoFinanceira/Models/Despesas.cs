namespace GestaoFinanceira.Models
{
    class Despesas : Transacao
    {
        public Despesas(string descricao, decimal valor, DateTime data, CategoriaEnum categoria) : base(descricao, valor, data, categoria)
        {
        }

        public override decimal CalcularImpactoSaldo()
        {
            return -Valor;
        }
    }
}
