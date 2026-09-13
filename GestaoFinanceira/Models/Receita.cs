namespace GestaoFinanceira.Models
{
    class Receita : Transacao
    {
        public Receita(string descricao, decimal valor, DateTime data, CategoriaEnum categoria) : base(descricao, valor, data, categoria)
        {
        }

        public override decimal CalcularImpactoSaldo()
        {
            return Valor;
        }
    }
}