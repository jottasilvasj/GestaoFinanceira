using System;
using System.Collections.Generic;
using GestaoFinanceira.Models;

namespace GestaoFinanceira.Interfaces
{
    public interface IGerenciadorFinanceiro
    {
        void AdicionarReceita(Transacao transacao);
        decimal ObterSaldoTotal();
        List<Transacao> ListarTodas();
        List<Transacao> FiltrarPorCategoria(CategoriaEnum categoria);
        List<Transacao> BuscarPorPeriodo(DateTime dataInicio, DateTime dataFim);
    }
}