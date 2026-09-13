using System.Collections.Generic;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Interfaces
{
    public interface IArmazenamentoService
    {
        bool SalvarTransacoes(List<Transacao> transacoes);
        List<Transacao> CarregarTransacoes();
    }
}