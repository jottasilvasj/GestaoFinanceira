namespace GestaoFinanceira.Models
{
    internal abstract class Transacao
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public CategoriaEnum categoria { get; set; }
        public Transacao(string descricao, decimal valor, DateTime data, CategoriaEnum categoria)
        {
            Descricao = descricao;
            Valor = valor;
            Data = data;
            this.categoria = categoria;
        }

        public abstract decimal CalcularImpactoSaldo();
    }

}
